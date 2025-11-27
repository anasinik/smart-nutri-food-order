using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Application.Common.Models.Order;
using FoodOrderApi.Infrastructure.Data;
using FoodOrderApi.src.Domain.Entities;
using FoodOrderApi.src.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace FoodOrderApi.Infrastructure.Orders
{
    public class OrderService: IOrderService
    {
        private readonly FoodOrderDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<OrderService> _logger;

        public OrderService(FoodOrderDbContext context, IHttpContextAccessor accessor, ILogger<OrderService> logger)
        {
            _context = context;
            _httpContextAccessor = accessor;
            _logger = logger;
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
        {
            var httpUser = _httpContextAccessor.HttpContext?.User;
            var customerIdStr = httpUser?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(customerIdStr) || !Guid.TryParse(customerIdStr, out var customerId))
            {
                _logger.LogError("CustomerId invalid or missing");
                throw new UnauthorizedAccessException("Invalid or missing customer ID in token.");
            }

            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Meal)
                .FirstOrDefaultAsync(c => c.UserId == customerId);

            var order = new Order
            {
                Status = OrderStatus.Pending,
                Items = [],
                Address = dto.Address,
                PaymentMethod = dto.PaymentMethod
            };

            foreach (var cartItem in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    Meal = cartItem.Meal,
                    Quantity = cartItem.Quantity
                };

                order.AddItem(orderItem);
            }

            _context.Orders.Add(order);

            cart.Items.Clear();

            await _context.SaveChangesAsync();

            return order;
        }

    }
}
