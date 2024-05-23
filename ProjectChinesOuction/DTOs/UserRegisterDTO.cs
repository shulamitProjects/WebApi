using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ProjectChinesOuction.Models.DTOs
{
    public class UserRegisterDTO
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }
        public string Password { get; set; }

        [MaxLength(15)]
        public string PhonNumber { get; set; }
        public string Email { get; set; }
        public EnumRoleUser Role { get; set; }

    }
}



