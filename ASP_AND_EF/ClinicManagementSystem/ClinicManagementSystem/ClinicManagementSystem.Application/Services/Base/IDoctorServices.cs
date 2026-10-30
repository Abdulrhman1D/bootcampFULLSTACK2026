using ClinicManagementSystem.Application.Dtos;

namespace ClinicManagementSystem.Services.Base
{
    public interface IDoctorServices
    {
        Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync();
        Task<DoctorDto?> GetDoctorDetailsByUuidAsync(string uuid);
        Task<DoctorUpdateDto?> GetDoctorByUuidAsync(string uuid);

        Task<IEnumerable<SpecialtyDto>> GetAllSpecialtiesAsync();
        Task<IEnumerable<JobDto>> GetAllJobsAsync();

        Task AddDoctorAsync(DoctorCreateDto doctorDto);
        Task UpdateDoctorAsync(DoctorUpdateDto doctorDto);
        Task DeleteDoctorAsync(string uuid);
    }
}
