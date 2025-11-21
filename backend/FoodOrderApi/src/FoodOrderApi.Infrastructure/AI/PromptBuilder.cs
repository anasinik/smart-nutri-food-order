using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.src.Domain.Entities;

namespace FoodOrderApi.Infrastructure.AI
{
    public class PromptBuilder : IPromptBuilder
    {
        public string BuildNutriPrompt(string question, List<Meal> meals)
        {
            var context = string.Join("\n\n", meals.Select(m =>
                $"{m.Name} — {m.Description}. Calories: {m.Calories}, Proteins: {m.Proteins}, Carbs: {m.Carbohydrates}, Vegan: {m.IsVegan}"
            ));

            // TODO: Think about prompt improvements
            return $"""
            You are a nutrition expert assistant.
            Use ONLY the information from the context below when answering.
            If the context does not contain enough information, say so.

            Context:
            {context}

            User question:
            {question}
            """;
        }
    }
}
