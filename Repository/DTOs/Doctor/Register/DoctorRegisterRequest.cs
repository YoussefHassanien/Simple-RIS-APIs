using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Doctor.Register
{
    public class DoctorRegisterRequest
    {
        [Required]
        public uint PersonId { get; set; }
        [Required]
        public required decimal Salary { get; set; }
        [MaxLength(3)]
        public string? CurrencyCode { get; set; } = null!;
        public string? Expertise { get; set; }
    }
}
