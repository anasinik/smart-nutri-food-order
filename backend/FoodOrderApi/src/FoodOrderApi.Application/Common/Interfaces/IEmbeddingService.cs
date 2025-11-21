
using FoodOrderApi.src.Domain.Entities;

namespace FoodOrderApi.Application.Common.Interfaces
{
    public interface IEmbeddingService
    {
        Task<float[]> CreateMealEmbeddingAsync(Meal meal);
        public Task<float[]> CreateEmbeddingForTextAsync(string text);
    }
}
