using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Application.Common.Models.Order;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController: ControllerBase
    {

        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddToCart([FromBody] CreateCartItemDto dto)
        {
            await _cartService.AddToCartAsync(dto);
            return Ok(new { message = "Item added to cart." });
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            var result = await _cartService.GetCartAsync();
            return Ok(result);
        }
    }
}
