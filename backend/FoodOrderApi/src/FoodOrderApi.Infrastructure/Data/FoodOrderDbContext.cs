using Microsoft.EntityFrameworkCore;
using FoodOrderApi.src.Domain.Entities;
using FoodOrderApi.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FoodOrderApi.Infrastructure.Data
{
    public class FoodOrderDbContext : IdentityDbContext<ApplicationUser>
    {
        public FoodOrderDbContext(DbContextOptions<FoodOrderDbContext> options)
            : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; } = null!;
        public DbSet<Meal> Meals { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
    }
}
