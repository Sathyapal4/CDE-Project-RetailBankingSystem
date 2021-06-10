using System.ComponentModel.DataAnnotations;

namespace AuthenticationModule.Models
{
    public class UserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8), MaxLength(16)]
        public string Password { get; set; }

        public Role Role { get; set; } = Role.Customer;
    }
}
