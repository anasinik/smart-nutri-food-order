using FoodOrderApi.src.Domain.Common;
using FoodOrderApi.src.Domain.Entities;

namespace FoodOrderApi.Domain.Entities
{
    public class CartItem : BaseAuditableEntity
    {
        public Guid MealId { get; set; } 
        public Meal Meal { get; set; } = null!;
        public int Quantity { get; set; }
    }

}
