using Core.DTOs.Study.Create;
using Core.DTOs.Study.Edit;

namespace Core.Interfaces.Services
{
    public interface IStudyService
    {
        Task<StudyCreateResponse?> CreateStudy(string patientPersonId, StudyCreateRequest request);
        Task<bool> CompleteStudy(string studyId, string doctorPersonId);
        Task<StudyCreateResponse?> EditStudy (string studyId, StudyEditRequest request, string patientPersonId);
        Task<bool> CancelStudy(string studyId, string patientPersonId);
    }
}
