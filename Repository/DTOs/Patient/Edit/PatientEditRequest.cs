using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Patient.Edit
{
    public class PatientEditRequest
    {
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [Phone]
        [MaxLength(15)]
        public string? MobileNumber { get; set; }

        [RegularExpression("^(m|f)$", ErrorMessage = "Gender must be 'm' or 'f'.")]
        public string? Gender { get; set; }

        public DateOnly? DateOfBirth { get; set; }
    }
}
