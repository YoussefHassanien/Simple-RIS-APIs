using Core.DTOs.Patient.Details;
using Core.DTOs.Patient.Register;

namespace Core.Interfaces.Services
{
    public interface IPatientService
    {
        Task<PatientDetailsResponse?> GetPatientDetails(uint personId);
        Task<PatientRegisterResponse?> AddPatient(PatientRegisterRequest request);
    }
}
