using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Application.Common.Models.AI;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController: ControllerBase
    {
        private readonly IChatService chatService;

        public AIController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        [HttpPost("question")]
        public async Task<IActionResult> AskQuestion([FromBody] QuestionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Question)) 
                return BadRequest("Question is required.");

            var answer = await this.chatService.AnswerUserQuestionAsync(dto.Question);
            return Ok(new { text = answer });
        }


    }
}
