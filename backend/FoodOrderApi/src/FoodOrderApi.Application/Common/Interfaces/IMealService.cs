
using FoodOrderApi.Application.Common.Models.Common;
using FoodOrderApi.Application.Common.Models.Meal;

namespace FoodOrderApi.Application.Common.Interfaces
{
    public interface IMealService
    {
        Task<Result<Guid>> CreateMealAsync(CreateMealDto dto);
        Task<UploadPhotoResult> UploadPhotoAsync(Guid mealId, byte[] fileData, string fileName);
        Task<Result<List<MealDetailsDto>>> GetAllMealsAsync();
    }
}
