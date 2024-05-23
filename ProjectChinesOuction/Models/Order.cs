using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.Models
{
    public class Order
    {
            public int OrderId { get; set; }
             ICollection<OrderItem> OrderItems { get; set; }
            public int UserId { get; set; }
            public User User { get; set; }
            public DateTime Date { get; set; }
            public int sum { get; set; }
    }
}