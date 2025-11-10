using FoodOrderApi.Application.Common.Interfaces;
using FoodOrderApi.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodOrderApi.Infrastructure.Identity
{

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly IJwtService _jwtService;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _jwtService = jwtService;
        }

        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user?.UserName;
        }


        public async Task<(Result Result, string UserId)> CreateUserAsync(RegisterUserDto dto)
        {
            // TODO: unique email!
            var user = new ApplicationUser
            {
                UserName = dto.Username,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Role = src.Domain.Enums.UserRole.Customer
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, user.Role.ToString());
            }

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<(Result Result, string Token)> LoginAsync(LoginUserDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user == null)
                return (Result.Failure(["Invalid username or password."]), string.Empty);

            var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!passwordValid)
                return (Result.Failure(["Invalid username or password."]), string.Empty);

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtService.GenerateToken(user.ToDomainUser(), roles);

            return (Result.Success(), token);
        }
    }
}
