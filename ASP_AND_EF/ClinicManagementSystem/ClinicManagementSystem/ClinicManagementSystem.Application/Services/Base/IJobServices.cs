using ClinicManagementSystem.Application.Dtos;

namespace ClinicManagementSystem.Services.Base
{
    public interface IJobServices
    {
        Task<IEnumerable<JobDto>> GetAllJobsAsync();
        Task<JobDto?> GetJobDetailsByUuidAsync(string uuid);
        Task<JobUpdateDto?> GetJobByUuidAsync(string uuid);

        Task AddJobAsync(JobCreateDto jobDto);
        Task UpdateJobAsync(JobUpdateDto jobDto);
        Task DeleteJobAsync(string uuid);
    }
}
