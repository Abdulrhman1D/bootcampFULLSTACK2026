using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetAllJobsAsync();

        Task<Job?> GetJobByUuidAsync(string uuid);

        Task AddJobAsync(Job job);

        Task UpdateJobAsync(Job job);

        Task DeleteJobAsync(string uuid);

        Task<bool> HasDoctorsAsync(int jobId);
    }
}
