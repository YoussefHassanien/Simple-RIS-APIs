using Core.Interfaces.Repositories;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PatientDataRepository(AppDbContext context) : BaseRepository<PatientData>(context), IPatientDataRepository
    {
        public async Task<PatientData?> GetByPersonId(uint personId) =>
           await _context!.PatientData.FirstOrDefaultAsync(pd => pd.PersonId == personId);
    }
}
