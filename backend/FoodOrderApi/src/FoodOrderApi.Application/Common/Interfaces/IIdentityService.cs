using FoodOrderApi.Application.Common.Models;

namespace FoodOrderApi.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<(Result<object> Result, string UserId)> CreateUserAsync(RegisterUserDto dto);
        Task<string?> GetUserNameAsync(string userId);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<bool> AuthorizeAsync(string userId, string policyName);
        Task<Result<string>> LoginAsync(LoginUserDto dto);

    }
}
