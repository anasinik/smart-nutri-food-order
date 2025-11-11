using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Infrastructure.Data;
using FoodOrderApi.Infrastructure.Identity;
using FoodOrderApi.Infrastructure.Restaurants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOrderApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FoodOrderDbContext>(options =>
                    options.UseNpgsql(GetConnectionString()));
            services
                    .AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<FoodOrderDbContext>()
                    .AddDefaultTokenProviders();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IRestaurantService, RestaurantService>();

            return services;
        }

        private static string GetConnectionString()
        {
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var port = Environment.GetEnvironmentVariable("DB_PORT");
            var database = Environment.GetEnvironmentVariable("DB_NAME");
            var username = Environment.GetEnvironmentVariable("DB_USERNAME");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            return $"Host={host};Port={port};Database={database};Username={username};Password={password}";
        }
    }
}
