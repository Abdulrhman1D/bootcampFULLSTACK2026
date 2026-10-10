using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();

        Task<Doctor?> GetDoctorByUuidAsync(string uuid);

        Task AddDoctorAsync(Doctor doctor);

        Task UpdateDoctorAsync(Doctor doctor);

        Task DeleteDoctorAsync(string uuid);

        Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync();

        Task<IEnumerable<Job>> GetAllJobsAsync();

        Task<bool> HasAppointmentsAsync(int doctorId);
    }
}
