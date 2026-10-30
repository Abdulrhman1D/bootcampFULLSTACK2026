using ClinicManagementSystem.Application.Dtos;

namespace ClinicManagementSystem.Services.Base
{
    public interface IPatientServices
    {
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto?> GetPatientDetailsByUuidAsync(string uuid);
        Task<PatientUpdateDto?> GetPatientByUuidAsync(string uuid);

        Task AddPatientAsync(PatientCreateDto patientDto);
        Task UpdatePatientAsync(PatientUpdateDto patientDto);
        Task DeletePatientAsync(string uuid);
    }
}
