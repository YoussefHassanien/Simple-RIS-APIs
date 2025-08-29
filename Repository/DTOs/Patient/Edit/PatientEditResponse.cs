namespace Core.DTOs.Patient.Edit
{
    public class PatientEditResponse
    {
        public string? Name { get; set; }
        public string? MobileNumber { get; set; }
        public string? Gender { get; set; }
        public string? SocialSecurityNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Email { get; set; }
    }
}
