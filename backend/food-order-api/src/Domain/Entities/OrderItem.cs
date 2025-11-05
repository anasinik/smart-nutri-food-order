using FoodOrderApi.src.Domain.Common;

namespace FoodOrderApi.src.Domain.Entities
{
    public class OrderItem : BaseAuditableEntity
    {
        public int Quantity { get; set; }

        public Meal Meal { get; set; } = null!;

        public Order? Order { get; set; }

        public decimal TotalPrice => Meal.Price * Quantity;
        public decimal TotalCalories => Meal.Calories * Quantity;
        public decimal TotalProteins => (decimal)Meal.Proteins * Quantity;
        public decimal TotalCarbohydrates => (decimal)Meal.Carbohydrates * Quantity;
        public decimal TotalSugars => (decimal)Meal.Sugars * Quantity;
    }
}
