namespace Core.Models
{
    public partial class PatientData
    {

        public int PersonId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

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

        public string? DoctorFirstName { get; set; }

        public string? DoctorLastName { get; set; }
    }
}


