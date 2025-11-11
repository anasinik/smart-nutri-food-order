
using FoodOrderApi.Application.Common.Models;

namespace FoodOrderApi.Application.Common.Interfaces
{
    public interface IRestaurantService
    {
        Task<Result> CreateRestaurantAsync(CreateRestaurantDto dto);
    }
}
