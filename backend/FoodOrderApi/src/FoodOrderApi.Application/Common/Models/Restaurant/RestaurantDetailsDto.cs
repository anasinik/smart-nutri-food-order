
namespace FoodOrderApi.Application.Common.Models.Restaurant
{
    public class RestaurantDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? PhotoPath { get; set; }
    }
}
