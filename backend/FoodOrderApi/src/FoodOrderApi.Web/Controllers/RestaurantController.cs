using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Application.Common.Models.Restaurant;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController: ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantDto dto)
        {
            var result = await _restaurantService.CreateRestaurantAsync(dto);

            if (!result.Succeeded)
                return BadRequest(result);
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

            var result = await _restaurantService.UploadPhotoAsync(id, fileBytes, file.FileName);

            if (!result.Succeeded)
                return BadRequest(new { result.Errors });

            return Ok(new { photoUrl = result.PhotoUrl });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _restaurantService.GetAllRestaurantsAsync();

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result.Data);
        }

    }
}
