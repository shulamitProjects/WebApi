namespace ProjectChinesOuction.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int GiftId { get; set; }
        public Gift Gift { get; set; }
        public int Count { get; set; }
        public EnumOrderStatus OrderItemStatus { get; set; }
    }
}
