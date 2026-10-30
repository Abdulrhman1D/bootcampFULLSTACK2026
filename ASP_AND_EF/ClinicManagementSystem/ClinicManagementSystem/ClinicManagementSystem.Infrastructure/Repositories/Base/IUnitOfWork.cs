using ClinicManagementSystem.Repositories;

namespace ClinicManagementSystem.Infrastructure.Repositories.Base
{
    public interface IUnitOfWork
    {
        IAppointmentRepository AppointmentRepository { get; }

        IDoctorRepository DoctorRepository { get; }

        IJobRepository JobRepository { get; }

        IPatientRepository PatientRepository { get; }

        ISpecialtyRepository SpecialtyRepository { get; }

        IUserRepository UserRepository { get; }

    }
}
