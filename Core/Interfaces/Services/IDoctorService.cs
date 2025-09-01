using Core.DTOs.Doctor.Edit;
using Core.DTOs.Doctor.Register;
using Core.DTOs.Doctor.Studies;

namespace Core.Interfaces.Services
{
    public interface IDoctorService
    {
        Task<DoctorRegisterResponse?> AddDoctor(DoctorRegisterRequest request);
        Task<DoctorEditResponse?> EditDoctor(DoctorEditRequest request, string personId);
        Task<List<DoctorStudiesResponse>>  GetDoctorStudies(string personId, uint page = 1, uint limit = 10);
    }
}
