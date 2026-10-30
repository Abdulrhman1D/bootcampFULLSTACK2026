using ClinicManagementSystem.Application.Dtos;


namespace ClinicManagementSystem.Services.Base
{
    public interface IAppointmentServices
    {
        Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();

        Task<AppointmentUpdateDto?> GetAppointmentByUuidAsync(string uuid);

        Task<AppointmentDto?> GetAppointmentDetailsByUuidAsync(string uuid);
 
        Task AddAppointmentAsync(AppointmentCreateDto appointmentDto);

        Task UpdateAppointmentAsync(AppointmentUpdateDto appointmentDto);

        Task DeleteAppointmentAsync(string uuid);

        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();

        Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync();
    }
}
