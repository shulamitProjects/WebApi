using ProjectChinesOuction.DAL;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ProjectChinesOuction.Models.DTOs
{
    public class WinnerDTO
    {
        public int GiftId { get; set; }
        public int UserId { get; set; }
        public int RaffleId { get; set; }
    }
}
