using Microsoft.EntityFrameworkCore;
using FoodOrderApi.src.Domain.Entities;

namespace FoodOrderApi.Infrastructure.Data
{
    public class FoodOrderDbContext : DbContext
    {
        public FoodOrderDbContext(DbContextOptions<FoodOrderDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Restaurant> Restaurants { get; set; } = null!;
        public DbSet<Meal> Meals { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
    }
}
