using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var (result, userId) = await _identityService.CreateUserAsync(dto);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Ok(new { userId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var result = await _identityService.LoginAsync(dto);

            if (!result.Succeeded)
                return Unauthorized(result);

            return Ok(new { token = result.Data });
        }

    }
}
