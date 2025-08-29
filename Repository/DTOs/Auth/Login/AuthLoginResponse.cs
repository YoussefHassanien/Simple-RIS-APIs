namespace Core.DTOs.Auth.Login
{
    public class AuthLoginResponse
    {
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string MobileNumber { get; set; }
        public string? Gender { get; set; }
        public string? SocialSecurityNumber { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public string? Role { get; set; }
        public required string AccessToken { get; set; }
    }
}
