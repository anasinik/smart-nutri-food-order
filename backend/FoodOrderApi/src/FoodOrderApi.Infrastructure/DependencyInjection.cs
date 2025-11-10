using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Infrastructure.Data;
using FoodOrderApi.Infrastructure.Identity;
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
            // connection
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var port = Environment.GetEnvironmentVariable("DB_PORT");
            var database = Environment.GetEnvironmentVariable("DB_NAME");
            var username = Environment.GetEnvironmentVariable("DB_USERNAME");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            var connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";

            // DbContext
            services.AddDbContext<FoodOrderDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Identity
            services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<FoodOrderDbContext>()
                .AddDefaultTokenProviders();

            // Services
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IJwtService, JwtService>();


            return services;
        }
    }
}
