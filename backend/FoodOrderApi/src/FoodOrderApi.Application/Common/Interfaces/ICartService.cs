
using FoodOrderApi.Application.Common.Models.Order;

namespace FoodOrderApi.Application.Common.Interfaces
{
    public interface ICartService
    {
        public Task AddToCartAsync(CreateCartItemDto dto);
        public Task<CartDto> GetCartAsync();
    }
}
