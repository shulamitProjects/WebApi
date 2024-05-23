using ProjectChinesOuction.DAL;
using ProjectChinesOuction.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectChinesOuction.Models.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int GiftId { get; set; }
        public ICollection<OrderItemDTO> OrderItems { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
