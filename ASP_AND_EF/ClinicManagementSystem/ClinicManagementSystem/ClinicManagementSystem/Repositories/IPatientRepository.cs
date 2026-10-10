using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();

        Task<Patient?> GetPatientByUuidAsync(string uuid);

        Task AddPatientAsync(Patient patient);

        Task UpdatePatientAsync(Patient patient);

        Task DeletePatientAsync(string uuid);

        Task<bool> HasAppointmentsAsync(int patientId);
    }
}
