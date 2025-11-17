using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Application.Common.Models.Common;
using FoodOrderApi.Application.Common.Models.Meal;
using FoodOrderApi.Infrastructure.Data;
using FoodOrderApi.src.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace FoodOrderApi.Infrastructure.Restaurants
{
    public class MealService : IMealService
    {
        private readonly FoodOrderDbContext _context;
        private readonly ILogger<MealService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MealService(
            FoodOrderDbContext context,
            ILogger<MealService> logger,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<Guid>> CreateMealAsync(CreateMealDto dto)
        {
            var httpUser = _httpContextAccessor.HttpContext?.User;
            var managerIdStr = httpUser.FindFirstValue(ClaimTypes.NameIdentifier);
            var managerUsername = httpUser.Identity?.Name;

            if (!Guid.TryParse(managerIdStr, out var managerGuid))
            {
                _logger.LogError("Could NOT parse ManagerId as GUID: {ManagerId}", managerIdStr);
                return Result<Guid>.Failure(["Invalid user ID format."]);
            }

            _logger.LogInformation("Parsed ManagerId GUID: {ManagerGuid}", managerGuid);
            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.ManagerId == managerGuid);

            // Create meal
            var meal = new Meal
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Calories = dto.Calories,
                Proteins = dto.Proteins,
                Carbohydrates = dto.Carbohydrates,
                Sugars = dto.Sugars,
                IsVegan = dto.IsVegan,
                PhotoPath = dto.PhotoPath,
                Restaurant = restaurant
            };

            restaurant.AddMeal(meal);

            _context.Meals.Add(meal);
            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Created new meal with ID: {MealId}", meal.Id);

            return Result<Guid>.Success(meal.Id);
        }

        public async Task<UploadPhotoResult> UploadPhotoAsync(Guid mealId, byte[] fileData, string fileName)
        {
            var meal = await _context.Meals.FindAsync(mealId);
            if (meal == null)
                return new UploadPhotoResult { Succeeded = false, Errors = ["Meal not found."] };

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/meals");
            Directory.CreateDirectory(uploadsFolder);

            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
            var filePath = Path.Combine(uploadsFolder, newFileName);

            await File.WriteAllBytesAsync(filePath, fileData);

            meal.PhotoPath = newFileName;
            await _context.SaveChangesAsync();

            return new UploadPhotoResult
            {
                Succeeded = true,
                PhotoUrl = $"/images/meals/{newFileName}"
            };
        }

        public Task<Result<List<MealDetailsDto>>> GetAllMealsAsync()
        {
            _logger.LogInformation("Fetching all meals from the database.");
            var meals = _context.Meals.Select(m => new MealDetailsDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                Calories = m.Calories,
                Proteins = m.Proteins,
                Carbohydrates = m.Carbohydrates,
                Sugars = m.Sugars,
                IsVegan = m.IsVegan,
                PhotoPath = m.PhotoPath,
                RestaurantName = m.Restaurant.Name
            }).ToListAsync();

            return meals
                .ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        _logger.LogError(task.Exception, "Error fetching meals from the database.");
                        return Result<List<MealDetailsDto>>.Failure(new[] { "An error occurred while fetching meals." });
                    }

                    return Result<List<MealDetailsDto>>.Success(task.Result);
                });
        }
    }
}
