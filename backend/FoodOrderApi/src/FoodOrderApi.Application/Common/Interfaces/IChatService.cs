
namespace FoodOrderApi.Application.Common.Interfaces
{
    public interface IChatService
    {
        public Task<string> AnswerUserQuestionAsync(string question);
    }
}
