namespace Core.DTOs.Doctor.Studies
{
    public class DoctorStudiesResponse
    {
        public required uint PatientPersonId { get; set; }
        public required string PatientName { get; set; }
        public required string PatientMobileNumber { get; set; }
        public required DateOnly PatientDateOfBirth { get; set; }
        public required uint ServiceId { get; set; }
        public required string ServiceType { get; set; }
        public required string ServiceDescription { get; set; }
        public required string ServiceCurrencyCode { get; set; }
        public required decimal ServiceCost { get; set; }
        public required string StudyStatus { get; set; }
        public DateTime? StudyDate {  get; set; }
    }
}
