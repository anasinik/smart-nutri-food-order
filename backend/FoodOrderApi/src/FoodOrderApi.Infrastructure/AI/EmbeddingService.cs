using Azure.AI.OpenAI;
using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.src.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenAI.Embeddings;

namespace FoodOrderApi.Infrastructure.Embedding
{
    public class EmbeddingService : IEmbeddingService
    {
        private readonly EmbeddingClient _embeddingClient;
        private readonly ILogger<EmbeddingService> _logger;

        public EmbeddingService(
            AzureOpenAIClient client,
            IConfiguration config,
            ILogger<EmbeddingService> logger
            )
        {
            var deployment = config["AzureOpenAI:EmbeddingDeployment"];
            _logger = logger;

            _logger.LogInformation("USING DEPLOYMENT: " + deployment);
            _embeddingClient = client.GetEmbeddingClient(deployment);
        }

        public async Task<float[]> CreateMealEmbeddingAsync(Meal meal)
        {
            var input =
                $"{meal.Name}. {meal.Description}. Calories: {meal.Calories}. Proteins: {meal.Proteins}. Carbs: {meal.Carbohydrates}. Vegan: {meal.IsVegan}.";

            _logger.LogInformation("GENERATING EMBEDDING FOR: " + input);
            
            OpenAIEmbeddingCollection result =
                await _embeddingClient.GenerateEmbeddingsAsync(new[] { input });

            OpenAIEmbedding embedding = result[0];

            return embedding.ToFloats().ToArray();
        }

        public async Task<float[]> CreateEmbeddingForTextAsync(string text)
        {
            _logger.LogInformation("GENERATING EMBEDDING FOR TEXT: " + text);

            OpenAIEmbeddingCollection result =
                await _embeddingClient.GenerateEmbeddingsAsync(new[] { text });

            OpenAIEmbedding embedding = result[0];

            return embedding.ToFloats().ToArray();
        }
    }
}
