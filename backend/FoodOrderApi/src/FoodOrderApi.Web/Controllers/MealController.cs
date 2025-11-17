using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Application.Common.Models.Meal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MealController: ControllerBase
    {
        private readonly IMealService _service;
    
        public MealController(IMealService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMealDto dto)
        {
            var result = await _service.CreateMealAsync(dto);

            if (!result.Succeeded)
                return new BadRequestObjectResult(result);

            return Ok(new { id = result.Data });
        }

        [HttpPost("{id}/photo")]
        public async Task<IActionResult> UploadPhoto(Guid id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var fileBytes = ms.ToArray();

            var result = await _service.UploadPhotoAsync(id, fileBytes, file.FileName);

            if (!result.Succeeded)
                return BadRequest(new { result.Errors });

            return Ok(new { photoUrl = result.PhotoUrl });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllMealsAsync();

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result.Data);
        }

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetById(Guid restaurantId)
        {
            var result = await _service.GetMealsByRestaurantAsync(restaurantId);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result.Data);
        }
    }
}
