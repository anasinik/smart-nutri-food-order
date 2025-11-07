using FoodOrderApi.src.Domain.Common;
using FoodOrderApi.src.Domain.Enums;

namespace FoodOrderApi.src.Domain.Entities
{
    public class Order : BaseAuditableEntity
    {
        public User User { get; set; } = null!;
        public Restaurant Restaurant { get; set; } = null!;
        public List<OrderItem> Items { get; set; } = [];
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
        public decimal TotalCalories => Items.Sum(item => item.TotalCalories);
        public decimal TotalProteins => Items.Sum(item => item.TotalProteins);
        public decimal TotalCarbohydrates => Items.Sum(item => item.TotalCarbohydrates);
        public decimal TotalSugars => Items.Sum(item => item.TotalSugars);
    }
}
