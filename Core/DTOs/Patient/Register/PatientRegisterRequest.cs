using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Patient.Register
{
    public class PatientRegisterRequest
    {
        [Required]
        [Range(1, uint.MaxValue, ErrorMessage = "PersonId must be a positive integer.")]
        public required uint PersonId { get; set; }

        
        public bool? IsVip { get; set; }
    }
}
