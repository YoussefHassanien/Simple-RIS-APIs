using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Study.Create
{
    public class StudyCreateRequest
    {
        [Required]
        [Range(1, uint.MaxValue, ErrorMessage = "DoctorId must be a positive integer.")]
        public uint DoctorId { get; set; }

        [Required]
        [Range(1, uint.MaxValue, ErrorMessage = "ServiceId must be a positive integer.")]
        public uint ServiceId { get; set; }
    }
}
