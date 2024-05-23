using System.Diagnostics.CodeAnalysis;

namespace ProjectChinesOuction.Models
{
    public class Winner
    {
        public int WinnerId { get; set; }

        [AllowNull]
        public int GiftId { get; set; }
        public Gift Gift { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        // [AllowNull]
        //[ForeignKey("Raffle")]
        public int RaffleId { get; set; }
        public Raffle Raffle { get; set; }
    }
}
