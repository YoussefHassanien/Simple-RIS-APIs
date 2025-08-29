using Core.Models;

namespace Core.Interfaces.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<Person?> GetByEmail(string email);
        Task<Person?> GetByMobileNumber(string mobileNumber);
        Task<Person?> GetBySocialSecurityNumber(string socialSecurityNumber);
    }
}
