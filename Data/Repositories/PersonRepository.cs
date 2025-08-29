using Core.Interfaces.Repositories;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PersonRepository(AppDbContext context) : BaseRepository<Person>(context), IPersonRepository
    {
        public async Task<Person?> GetByEmail(string email) =>
            await _context!.Persons.FirstOrDefaultAsync(p => p.Email == email);
        public async Task<Person?> GetByMobileNumber(string mobileNumber) =>
           await _context!.Persons.FirstOrDefaultAsync(p => p.MobileNumber == mobileNumber);
        public async Task<Person?> GetBySocialSecurityNumber(string socialSecurityNumber) =>
           await _context!.Persons.FirstOrDefaultAsync(p => p.SocialSecurityNumber == socialSecurityNumber);
    }
}
