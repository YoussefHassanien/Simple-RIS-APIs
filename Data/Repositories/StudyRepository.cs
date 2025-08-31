using Core.Interfaces.Repositories;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class StudyRepository(AppDbContext context) : BaseRepository<Study>(context), IStudyRepository
    {
        public async Task<List<Study>> GetByDoctorId(uint doctorId, uint page = 1, uint limit = 10, string[]? includes = null)
        {
            IQueryable<Study> query = _context.Studies.Where(s => s.DoctorId == (int)doctorId);
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            return await query.Skip((int)((page - 1) * limit)).Take((int)limit).ToListAsync();
        }

    }
}
