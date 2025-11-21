using FoodOrderApi.src.Domain.Entities;

namespace FoodOrderApi.Application.Common.Interfaces
{
    public interface IRetrievalService
    {
        Task<List<Meal>> GetRelevantMealsAsync(float[] queryEmbedding, int topN = 5);
    }
}
