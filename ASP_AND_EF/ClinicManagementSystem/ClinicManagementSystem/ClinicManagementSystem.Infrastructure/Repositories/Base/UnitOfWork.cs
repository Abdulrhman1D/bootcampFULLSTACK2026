using ClinicManagementSystem.Infrastructure.Data;
using ClinicManagementSystem.Repositories;

namespace ClinicManagementSystem.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _db;


        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            AppointmentRepository = new AppointmentRepository(_db);
            DoctorRepository = new DoctorRepository(_db);
            JobRepository = new JobRepository(_db);
            PatientRepository = new PatientRepository(_db);
            SpecialtyRepository = new SpecialtyRepository(_db);
            UserRepository = new UserRepository(_db);
        }

        public IAppointmentRepository AppointmentRepository { get; }

        public IDoctorRepository DoctorRepository { get; }

        public IJobRepository JobRepository { get; }

        public IPatientRepository PatientRepository { get; }

        public ISpecialtyRepository SpecialtyRepository { get; }

        public IUserRepository UserRepository { get; }
    }
}
