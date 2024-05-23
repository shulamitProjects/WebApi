using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ProjectChinesOuction.Models
{
    public class User
    {
        [Key, NotNull]
        public int UserId { get; set; }

        [NotNull]
        public string Password { get; set; }

        [NotNull]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [NotNull]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(15)]
        public string PhonNumber { get; set; }

        public string Email { get; set; }
        public EnumRoleUser Role { get; set; }
        ICollection<Order> Orders { get; set; }
    }
}
