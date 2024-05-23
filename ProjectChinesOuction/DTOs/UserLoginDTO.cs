using System.ComponentModel.DataAnnotations;

namespace ProjectChinesOuction.Models.DTOs
{
    public class UserLoginDTO
    {
       // [EmailAddress]
        public string Email { get; set; }
       // [Required]
        public string Password { get; set; }
    }
}