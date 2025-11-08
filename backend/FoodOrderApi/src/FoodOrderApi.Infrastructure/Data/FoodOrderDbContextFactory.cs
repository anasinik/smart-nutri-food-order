using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApi.Infrastructure.Data
{
    public class FoodOrderDbContextFactory : IDesignTimeDbContextFactory<FoodOrderDbContext>
    {
        public FoodOrderDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FoodOrderDbContext>();

            var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
            var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "foodorder";
            var username = Environment.GetEnvironmentVariable("DB_USERNAME") ?? "postgres";
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "password";

            var connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";

            optionsBuilder.UseNpgsql(connectionString);

            return new FoodOrderDbContext(optionsBuilder.Options);
        }
    }
}
