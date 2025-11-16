using FoodOrderApi.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodOrderApi.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result<object> ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result<object>.Success(null)
                : Result<object>.Failure(result.Errors.Select(e => e.Description));
        }

    }
}
