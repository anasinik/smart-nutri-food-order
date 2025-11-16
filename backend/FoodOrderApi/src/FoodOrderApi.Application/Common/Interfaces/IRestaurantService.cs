using FoodOrderApi.Application.Common.Models.Common;
using FoodOrderApi.Application.Common.Models.Restaurant;
using FoodOrderApi.Application.Common.Models.User;

namespace FoodOrderApi.Application.Common.Interfaces
{
    public interface IRestaurantService
    {
        Task<Result<Guid>> CreateRestaurantAsync(CreateRestaurantDto dto);
        Task<UploadPhotoResult> UploadPhotoAsync(Guid restaurantId, byte[] fileData, string fileName);
        Task<Result<List<RestaurantDetailsDto>>> GetAllRestaurantsAsync();
    }
}
