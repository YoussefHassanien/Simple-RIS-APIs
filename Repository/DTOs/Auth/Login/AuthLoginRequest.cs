using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Auth.Login
{
    public class AuthLoginRequest
    {
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public required string Email { get; set; }
        [Required]
        [MaxLength(256)]
        public required string Password { get; set; }
    }
}
