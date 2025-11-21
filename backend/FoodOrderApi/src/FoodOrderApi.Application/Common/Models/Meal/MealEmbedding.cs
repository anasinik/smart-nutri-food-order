
using FoodOrderApi.src.Domain.Common;

namespace FoodOrderApi.Application.Common.Models.Meal
{
    public class MealEmbedding : BaseAuditableEntity
    {
        public Guid MealId { get; set; }
        public float[] Embedding { get; set; }
    }
}
