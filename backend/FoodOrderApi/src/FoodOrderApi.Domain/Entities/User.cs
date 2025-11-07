using FoodOrderApi.src.Domain.Common;
using FoodOrderApi.src.Domain.Enums;

namespace FoodOrderApi.src.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Customer;

    }
}
