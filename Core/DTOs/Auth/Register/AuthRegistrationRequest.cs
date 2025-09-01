using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Auth.Register
{
    public class AuthRegistrationRequest
    {
        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public required string LastName { get; set; }

        [Required]
        [Phone]
        [MaxLength(15)]
        public required string MobileNumber { get; set; }

        [Required]
        [RegularExpression("^(m|f)$", ErrorMessage = "Gender must be 'm' or 'f'.")]
        public required string Gender { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14)]
        public required string SocialSecurityNumber { get; set; }

        [Required]
        public required DateOnly DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public required string Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(256)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\w\d\s]).+$", ErrorMessage = "Password must contain at least one uppercase letter, one digit, and one symbol.")]
        public required string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public required string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(20)]
        [RegularExpression("^(patient|doctor)$", ErrorMessage = "Role must be 'patient' or 'doctor'.")]
        public required string Role { get; set; }
    }
}
