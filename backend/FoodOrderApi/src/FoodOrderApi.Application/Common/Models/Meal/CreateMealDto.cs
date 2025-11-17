
namespace FoodOrderApi.Application.Common.Models.Meal
{
    public class CreateMealDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Calories { get; set; }
        public double Proteins { get; set; }
        public double Carbohydrates { get; set; }
        public double Sugars { get; set; }
        public bool IsVegan { get; set; }
        public string PhotoPath { get; set; } = string.Empty;
    }
}
