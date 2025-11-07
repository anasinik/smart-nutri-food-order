using FoodOrderApi.src.Domain.Common;

namespace FoodOrderApi.src.Domain.Entities
{
    public class Restaurant : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public User Manger { get; set; } = null!;

        public List<Meal> Menu { get; set; } = [];

        public void AddMeal(Meal meal)
        {
            Menu.Add(meal);
        }
    }
}
