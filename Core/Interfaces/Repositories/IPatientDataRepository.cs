using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IPatientDataRepository : IBaseRepository<PatientData>
    {
        Task<PatientData?> GetByPersonId(uint personId);
    }
}
