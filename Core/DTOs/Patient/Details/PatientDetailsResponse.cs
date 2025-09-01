namespace Core.DTOs.Patient.Details
{
    public class PatientDetailsResponse
    {
        public required int PersonId { get; set; }

        public string PatientName { get; set; } = null!;

        public string MobileNumber { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? SocialSecurityNumber { get; set; }

        public int PatientId { get; set; }

        public bool? IsVip { get; set; }

        public bool? IsActive { get; set; }

        public int? StudyId { get; set; }

        public DateTime? StudyCreatedAt { get; set; }

        public DateTime? StudyUpdatedAt { get; set; }

        public int? ServiceId { get; set; }

        public string? ServiceType { get; set; }

        public string? ServiceDescription { get; set; }

        public decimal? ServiceCost { get; set; }

        public string? ServiceCurrency { get; set; }

        public int? DoctorId { get; set; }

        public string? DoctorName { get; set; }

    }
}
