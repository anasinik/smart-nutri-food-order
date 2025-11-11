
using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Application.Common.Models;
using FoodOrderApi.Infrastructure.Data;
using FoodOrderApi.Infrastructure.Identity;
using FoodOrderApi.src.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FoodOrderApi.Infrastructure.Restaurants
{
    public class RestaurantService : IRestaurantService
    {
        private readonly FoodOrderDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RestaurantService(FoodOrderDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<Result> CreateRestaurantAsync(CreateRestaurantDto dto)
        {
            var manager = await _userManager.FindByIdAsync(dto.ManagerId);
            if (manager is null)
                return Result.Failure(new[] { "Manager user not found." });

            if (!await _userManager.IsInRoleAsync(manager, "Manager"))
            {
                var roleResult = await _userManager.AddToRoleAsync(manager, "Manager");
                if (!roleResult.Succeeded)
                    return Result.Failure(roleResult.Errors.Select(e => e.Description));
            }

            var restaurant = new Restaurant
            {
                Name = dto.Name,
                Address = dto.Address,
                Description = dto.Description,
                PhoneNumber = dto.PhoneNumber,
                MangerId = Guid.Parse(manager!.Id)
            };


            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return Result.Success();
        }

    }
}
