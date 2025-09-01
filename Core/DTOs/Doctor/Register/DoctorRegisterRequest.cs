using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Doctor.Register
{
    public class DoctorRegisterRequest
    {
        [Required]
        [Range(1, uint.MaxValue, ErrorMessage = "PersonId must be a positive integer.")]
        public uint PersonId { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Salary must be positive.")]
        public required decimal Salary { get; set; }
        [MaxLength(3)]
        public string? CurrencyCode { get; set; } = null!;
        public string? Expertise { get; set; }
    }
}
