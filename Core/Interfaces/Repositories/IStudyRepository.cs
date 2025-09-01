using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IStudyRepository : IBaseRepository<Study>
    {
        Task<List<Study>> GetByDoctorId(uint doctorId, uint page = 1, uint limit = 10, string[]? includes = null);
    }
}
