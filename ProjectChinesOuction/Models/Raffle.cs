using ProjectChinesOuction.DAL;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ProjectChinesOuction.Models
{
    public class Raffle
    {
        //[Key]
        public int RaffleId { get; set; }
        [AllowNull]
        public DateTime RaffleDate { get; set; }
        [AllowNull]
        public int UserId { get; set; }
        [AllowNull]
        public User user { get; set; }
        [AllowNull]
        public int PresentId { get; set; }
        [AllowNull]
        public Gift present { get; set; }
    }
}
