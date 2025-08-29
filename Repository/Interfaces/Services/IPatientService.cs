using Core.DTOs.Patient.Details;

namespace Core.Interfaces.Services
{
    public interface IPatientService
    {
        Task<PatientDetailsResponse?> GetPatientDetails(uint personId);
    }
}
