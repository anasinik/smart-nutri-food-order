using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Application.Common.Models;
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

            return Ok(result);
        }
    }
}
