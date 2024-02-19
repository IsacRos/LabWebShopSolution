using System.ComponentModel.DataAnnotations;

namespace LabWebShop.Models
{
    public class User
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
