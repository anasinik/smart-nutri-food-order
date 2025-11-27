
namespace FoodOrderApi.Application.Common.Models.Order
{
    public class CartItemDto
    {
        public string? MealName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
