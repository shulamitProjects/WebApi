using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace ProjectChinesOuction.Models
{
    public class Gift
 
    {
        [Key]
        [NotNull]

        public int GiftId { get; set; }
        public string Name { get; set; }

        [NotNull]
        public int DonorId { get; set; }

        [NotNull]
        public Donor Donor { get; set; }

        [NotNull]
        public EnumGiftCategory Category { get; set; }

        public double Price { get; set; }

        public string Image_Url { get; set; }
    }
}
