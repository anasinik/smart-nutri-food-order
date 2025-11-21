using FoodOrderApi.Application.Common.Interfaces;
using Azure.AI.OpenAI;
using OpenAI.Chat;
using Microsoft.Extensions.Logging;

namespace FoodOrderApi.Infrastructure.AI
{

    public class ChatService : IChatService
    {
        private readonly IEmbeddingService _embeddingService;
        private readonly IRetrievalService _retrievalService;
        private readonly IPromptBuilder _promptBuilder;
        private readonly AzureOpenAIClient _client;
        private readonly ILogger<ChatService> _logger;

        private const string DeploymentName = "gpt-4o";

        public ChatService(
            IEmbeddingService embeddingService,
            IRetrievalService retrievalService,
            IPromptBuilder promptBuilder,
            AzureOpenAIClient client,
            ILogger<ChatService> logger)
        {
            _embeddingService = embeddingService;
            _retrievalService = retrievalService;
            _promptBuilder = promptBuilder;
            _client = client;
            _logger = logger;
        }

        public async Task<string> AnswerUserQuestionAsync(string question)
        {
            _logger.LogInformation(">>>>>>>>>>>>>Answering question: " + question);
            var embedding = await CreateSafeEmbeddingAsync(question);
            var relevantMeals = await _retrievalService.GetRelevantMealsAsync(embedding, topN: 5);

            var prompt = _promptBuilder.BuildNutriPrompt(question, relevantMeals);

            var chatClient = _client.GetChatClient(DeploymentName);

            ChatCompletion response = await chatClient.CompleteChatAsync(
                [
                    new UserChatMessage(prompt)
                ]);

            return response.Content[0].Text;

        }

        private async Task<float[]> CreateSafeEmbeddingAsync(string text)
        {
            try
            {
                return await _embeddingService.CreateEmbeddingForTextAsync(text);
            }
            catch
            {
                return GenerateRandomEmbedding(1536);
            }
        }

        private float[] GenerateRandomEmbedding(int size)
        {
            var rnd = new Random();
            var arr = new float[size];

            for (int i = 0; i < size; i++)
                arr[i] = (float)rnd.NextDouble();

            return arr;
        }
    }
}
