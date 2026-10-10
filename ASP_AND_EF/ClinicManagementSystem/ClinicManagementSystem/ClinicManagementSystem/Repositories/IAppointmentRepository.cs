using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>>GetAllAppointmentsAsync();

        Task<Appointment?>GetAppointmentByUuidAsync(string uuid);

        Task AddAppointmentAsync(Appointment appointment);

        Task UpdateAppointmentAsync(Appointment appointment);

        Task DeleteAppointmentAsync(string uuid);

        Task<IEnumerable<Patient>> GetAllPatientsAsync();

        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
    }
}
