
using FoodOrderApi.Application.Common.Models.Order;
using FoodOrderApi.src.Domain.Entities;

namespace FoodOrderApi.Application.Common.Interfaces
{
    public interface IOrderService
    {
        public Task<Order> CreateOrderAsync(CreateOrderDto dto);
    }
}
