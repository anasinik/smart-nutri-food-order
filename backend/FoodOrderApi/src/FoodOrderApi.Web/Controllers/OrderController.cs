using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Application.Common.Models.Order;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _orderService;
        
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            await _orderService.CreateOrderAsync(dto);
            return Ok(new { message = "Order created" });
        }
    }
}
