using Core.DTOs.Patient.Details;
using Core.DTOs.Patient.Edit;
using Core.DTOs.Patient.Register;

namespace Core.Interfaces.Services
{
    public interface IPatientService
    {
        Task<PatientDetailsResponse?> GetPatientDetails(string personId);
        Task<PatientRegisterResponse?> AddPatient(PatientRegisterRequest request);
        Task<PatientEditResponse?> EditPatient(PatientEditRequest request, string personId);
        Task<bool> DeletePatient(string personId);
    }
}
