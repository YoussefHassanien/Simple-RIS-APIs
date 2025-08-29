using Core.Models;

namespace Core.DTOs.Patient.Register
{
    public class PatientRegisterRequest
    {
        public required uint PersonId { get; set; }
        public bool? IsVip { get; set; }
    }
}
