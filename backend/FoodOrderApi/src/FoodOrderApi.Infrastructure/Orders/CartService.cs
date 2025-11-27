using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Application.Common.Models.Order;
using FoodOrderApi.Domain.Entities;
using FoodOrderApi.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace FoodOrderApi.Infrastructure.Orders
{
    public class CartService: ICartService
    {
        private readonly FoodOrderDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CartService> _logger;

        public CartService(
            FoodOrderDbContext context,
            IHttpContextAccessor httpContextAccessor,
            ILogger<CartService> logger
            )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async Task AddToCartAsync(CreateCartItemDto dto)
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

            _logger.LogInformation("Cart fetch result: {HasCart}", cart != null);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = customerId,
                    Items = []
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            _logger.LogInformation("Fetching meal with Id={MealId}", dto.MealId);

            var meal = await _context.Meals.FirstOrDefaultAsync(m => m.Id == dto.MealId) 
                ?? throw new KeyNotFoundException($"Meal with Id {dto.MealId} not found.");


            var existingItem = cart.Items.FirstOrDefault(i => i.MealId == dto.MealId);

            if (existingItem == null)
            {
                var newItem = new CartItem
                {
                    MealId = dto.MealId,
                    Quantity = dto.Quantity
                };

                _logger.LogInformation("New CartItem: MealId={MealId}, Qty={Qty}", newItem.MealId, newItem.Quantity);

                cart.Items.Add(newItem);
                _context.Entry(newItem).State = EntityState.Added;
            }
            else
            {
                _logger.LogInformation("Item EXISTS. Increasing quantity: {OldQty} + {AddQty}",
                    existingItem.Quantity, dto.Quantity);

                existingItem.Quantity += dto.Quantity;
            }
            await _context.SaveChangesAsync();
        }

        public Task<CartDto> GetCartAsync()
        {
            var httpUser = _httpContextAccessor.HttpContext?.User;
            var customerIdStr = httpUser?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(customerIdStr) || !Guid.TryParse(customerIdStr, out var customerId))
            {
                throw new UnauthorizedAccessException("Invalid or missing customer ID in token.");
            }

            var cart = _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Meal)
                .FirstOrDefault(c => c.UserId == customerId);

            var cartDto = new CartDto
            {
                Items = cart?.Items.Select(i => new CartItemDto
                {
                    MealName = i.Meal.Name,
                    Quantity = i.Quantity,
                    Price = i.Meal.Price
                }).ToList() ?? []
            };

            return Task.FromResult(cartDto);
        }
    }
}
