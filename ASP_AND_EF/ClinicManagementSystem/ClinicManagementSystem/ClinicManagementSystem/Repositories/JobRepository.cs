using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly AppDbContext _context;

        public JobRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            IEnumerable<Job> jobs =
                await _context.Jobs.ToListAsync();

            return jobs;
        }

        public async Task<Job?> GetJobByUuidAsync(string uuid)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(
                j => j.Uuid == uuid
            );

            return job;
        }

        public Task AddJobAsync(Job job)
        {
            _context.Jobs.Add(job);

            return _context.SaveChangesAsync();
        }

        public Task UpdateJobAsync(Job job)
        {
            _context.Jobs.Update(job);

            return _context.SaveChangesAsync();
        }

        public Task DeleteJobAsync(string uuid)
        {
            var job = _context.Jobs.FirstOrDefault(
                j => j.Uuid == uuid
            );

            if (job != null)
            {
                _context.Jobs.Remove(job);

                return _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }

        public async Task<bool> HasDoctorsAsync(int jobId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(
                d => d.JobId == jobId
            );

            return doctor != null;
        }
    }
}
