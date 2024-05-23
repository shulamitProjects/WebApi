using ProjectChinesOuction.DAL;
using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.Models.DTOs
{
    public class RaffleDTO
    {
        public int RaffleId { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
        public int PresentId { get; set; }
        public Gift present { get; set; }
    }
}
