using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Doctor.Register
{
    public class DoctorRegisterResponse
    {
        public required int PersonId { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string MobileNumber { get; set; }
        public string? Gender { get; set; }
        public string? SocialSecurityNumber { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required decimal Salary { get; set; }
        public required string CurrencyCode { get; set; } = null!;
        public string? Expertise { get; set; }
    }
}
