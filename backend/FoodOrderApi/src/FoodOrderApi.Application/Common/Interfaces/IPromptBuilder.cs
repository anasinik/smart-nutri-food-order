using FoodOrderApi.src.Domain.Entities;

namespace FoodOrderApi.Application.Common.Interfaces
{
    public interface IPromptBuilder
    {
        string BuildNutriPrompt(string question, List<Meal> meals);
    }
}
