using Core.Models;
using Core;
using Core.Interfaces.Repositories;
using Data.Repositories;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IBaseRepository<Patient> Patients { get; private set; }
        public IPersonRepository Persons { get; private set; }
        public IBaseRepository<Doctor> Doctors { get; private set; }
        public IStudyRepository Studies { get; private set; }
        public IBaseRepository<Service> Services { get; private set; }
        public IPatientDataRepository PatientData { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Patients = new BaseRepository<Patient>(_context);
            Persons = new PersonRepository(_context);
            Doctors = new BaseRepository<Doctor>(_context);
            Studies = new StudyRepository(_context);
            Services = new BaseRepository<Service>(_context);
            PatientData = new PatientDataRepository(_context);

        }

        public int Complete() => _context.SaveChanges();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
