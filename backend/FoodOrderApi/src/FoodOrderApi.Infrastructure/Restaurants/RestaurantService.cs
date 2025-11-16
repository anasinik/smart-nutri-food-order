
using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Application.Common.Models;
using FoodOrderApi.Infrastructure.Data;
using FoodOrderApi.Infrastructure.Identity;
using FoodOrderApi.src.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
using Sprache;

namespace FoodOrderApi.Infrastructure.Restaurants
{
    public class RestaurantService : IRestaurantService
    {
        private readonly FoodOrderDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(
            FoodOrderDbContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<RestaurantService> logger
            )
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<Result<Guid>> CreateRestaurantAsync(CreateRestaurantDto dto)
        {
            _logger.LogInformation("Attempting to create restaurant for manager username: {ManagerUsername}", dto.ManagerUsername);

            var manager = await _userManager.FindByNameAsync(dto.ManagerUsername);
            if (manager is null)
            {
                _logger.LogWarning("Manager with username {ManagerUsername} not found.", dto.ManagerUsername);
                return Result<Guid>.Failure(new[] { "Manager user not found." });
            }

            if (!await _userManager.IsInRoleAsync(manager, "Manager"))
            {
                _logger.LogInformation("User {ManagerUsername} is not in Manager role. Attempting to add.", dto.ManagerUsername);
                var roleResult = await _userManager.AddToRoleAsync(manager, "Manager");
                if (!roleResult.Succeeded)
                {
                    _logger.LogError("Failed to add user {ManagerUsername} to Manager role: {Errors}",
                             dto.ManagerUsername,
                             string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    return Result<Guid>.Failure(roleResult.Errors.Select(e => e.Description));
                }
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

            _logger.LogInformation("Restaurant {Name} created successfully.", restaurant.Name);
            return Result<Guid>.Success(restaurant.Id);
        }

        public async Task<UploadPhotoResult> UploadPhotoAsync(Guid restaurantId, byte[] fileData, string fileName)
        {
            var restaurant = await _context.Restaurants.FindAsync(restaurantId);
            if (restaurant == null)
                return new UploadPhotoResult { Succeeded = false, Errors = new[] { "Restaurant not found." } };

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/restaurants");
            Directory.CreateDirectory(uploadsFolder);

            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
            var filePath = Path.Combine(uploadsFolder, newFileName);

            await File.WriteAllBytesAsync(filePath, fileData);

            restaurant.PhotoPath = newFileName;
            await _context.SaveChangesAsync();

            return new UploadPhotoResult
            {
                Succeeded = true,
                PhotoUrl = $"/images/restaurants/{newFileName}"
            };
        }



    }
}
