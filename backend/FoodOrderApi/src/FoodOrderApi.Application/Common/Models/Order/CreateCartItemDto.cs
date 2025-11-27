
namespace FoodOrderApi.Application.Common.Models.Order
{
    public class CreateCartItemDto
    {
        public int Quantity { get; set; }
        public Guid MealId { get; set; }
    }
}
