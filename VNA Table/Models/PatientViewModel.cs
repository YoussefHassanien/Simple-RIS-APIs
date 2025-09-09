namespace VNA_Table.Models
{
    public class PatientViewModel
    {
        public int PersonId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
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
        
        // Computed properties
        public int Age => DateTime.Now.Year - DateOfBirth.Year - (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
        public string Status => IsActive == true ? "Active" : "Inactive";
        public string VipStatus => IsVip == true ? "VIP" : "Regular";
    }
}