using Core.Interfaces;

namespace Core.DTOs.Patient.Register
{
    public class PatientRegisterResponse
    {
        public required int PersonId { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string MobileNumber { get; set; }
        public string? Gender { get; set; }
        public string? SocialSecurityNumber { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public bool? IsVip { get; set; }
    }
}
