using FoodOrderApi.src.Domain.Common;

namespace FoodOrderApi.Domain.Entities
{
    public class Cart: BaseAuditableEntity
    {
        public List<CartItem> Items { get; set; } = null!;
        public Guid UserId { get; set; }
    }
}
