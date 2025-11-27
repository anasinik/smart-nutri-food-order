using Microsoft.EntityFrameworkCore;
using FoodOrderApi.src.Domain.Entities;
using FoodOrderApi.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FoodOrderApi.Application.Common.Models.Meal;
using FoodOrderApi.Domain.Entities;
using FoodOrderApi.src.Domain.Common;

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
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!; 

        public DbSet<MealEmbedding> MealEmbeddings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CartItem>()
                .HasKey(ci => ci.Id);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Created = DateTimeOffset.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModified = DateTimeOffset.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
