using Core.Interfaces.Repositories;
using Core.Models;

namespace Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Patient> Patients { get; }
        IPersonRepository Persons { get; }
        IBaseRepository<Doctor> Doctors { get; }
        IBaseRepository<Study> Studies { get; }
        IBaseRepository<Service> Services { get; }
        IPatientDataRepository PatientData { get; }

        int Complete();
    }
}
