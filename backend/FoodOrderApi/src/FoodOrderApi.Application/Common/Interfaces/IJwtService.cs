
using FoodOrderApi.src.Domain.Entities;

namespace FoodOrderApi.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user, IList<string> roles);
    }
}
