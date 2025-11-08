using FoodOrderApi.src.Domain.Entities;
using FoodOrderApi.src.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace FoodOrderApi.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Customer;

        public User ToDomainUser()
        {
            return new User
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Username = this.Username,
                Password = this.PasswordHash ?? string.Empty,
                Role = Enum.Parse<UserRole>(this.Role.ToString(), ignoreCase: true)
            };
        }
    }
}
