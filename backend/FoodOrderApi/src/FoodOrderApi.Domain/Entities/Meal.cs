using FoodOrderApi.src.Domain.Common;

namespace FoodOrderApi.src.Domain.Entities
{
    public class Meal : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        // Nutritional information
        public int Calories { get; set; }
        public double Proteins { get; set; }      // grams
        public double Carbohydrates { get; set; } // grams
        public double Sugars { get; set; }        // grams

        public bool IsVegan { get; set; }
        public string PhotoPath { get; set; } = string.Empty;

        public Restaurant? Restaurant { get; set; }
    }
}
