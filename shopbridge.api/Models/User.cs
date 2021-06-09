using System.ComponentModel.DataAnnotations;

namespace shopbridge.api.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(200, ErrorMessage = "Must be between 8 and 200 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(200, ErrorMessage = "Must be between 8 and 200 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
        public string Token { get; set; }
    }
}
