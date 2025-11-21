using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Infrastructure.Data;
using FoodOrderApi.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApi.Infrastructure.AI
{
    public class RetrievalService : IRetrievalService
    {
        private readonly FoodOrderDbContext _context;

        public RetrievalService(FoodOrderDbContext context)
        {
            _context = context;
        }

        public async Task<List<Meal>> GetRelevantMealsAsync(float[] queryEmbedding, int topN = 5)
        {
            var mealEmbeddings = await _context.MealEmbeddings.AsNoTracking().ToListAsync();

            var mealIds = mealEmbeddings.Select(e => e.MealId).Distinct();
            var meals = await _context.Meals
                .Where(m => mealIds.Contains(m.Id))
                .ToDictionaryAsync(m => m.Id);

            return mealEmbeddings
                .Where(e => meals.ContainsKey(e.MealId))
                .Select(e => new
                {
                    Meal = meals[e.MealId],
                    Sim = CosineSimilarity(queryEmbedding, e.Embedding)
                })
                .OrderByDescending(x => x.Sim)
                .Take(topN)
                .Select(x => x.Meal)
                .ToList();
        }

        private float CosineSimilarity(float[] v1, float[] v2)
        {
            if (v1 == null || v2 == null || v1.Length != v2.Length)
                return 0f;

            float dot = 0f, m1 = 0f, m2 = 0f;

            for (int i = 0; i < v1.Length; i++)
            {
                dot += v1[i] * v2[i];
                m1 += v1[i] * v1[i];
                m2 += v2[i] * v2[i];
            }

            if (m1 == 0 || m2 == 0)
                return 0f;

            return dot / (float)(Math.Sqrt(m1) * Math.Sqrt(m2));
        }
    }
}
