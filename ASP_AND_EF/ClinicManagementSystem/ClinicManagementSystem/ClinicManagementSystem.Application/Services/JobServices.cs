using ClinicManagementSystem.Application.Dtos;
using ClinicManagementSystem.Domain.Models;
using ClinicManagementSystem.Infrastructure.Repositories.Base;
using ClinicManagementSystem.Services.Base;

namespace ClinicManagementSystem.Services
{
    public class JobServices : IJobServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<JobDto>> GetAllJobsAsync()
        {
            var jobs = await _unitOfWork.JobRepository
                .GetAllJobsAsync();

            return jobs.Select(j => new JobDto
            {
                Id = j.Id,
                Uuid = j.Uuid,
                Name = j.Name
            });
        }

        public async Task<JobDto?>
            GetJobDetailsByUuidAsync(string uuid)
        {
            var job = await _unitOfWork.JobRepository
                .GetJobByUuidAsync(uuid);

            if (job == null)
                return null;

            return new JobDto
            {
                Id = job.Id,
                Uuid = job.Uuid,
                Name = job.Name
            };
        }

        public async Task<JobUpdateDto?>
            GetJobByUuidAsync(string uuid)
        {
            var job = await _unitOfWork.JobRepository
                .GetJobByUuidAsync(uuid);

            if (job == null)
                return null;

            return new JobUpdateDto
            {
                Uuid = job.Uuid,
                Name = job.Name
            };
        }

        public async Task AddJobAsync(JobCreateDto jobDto)
        {
            var job = new Job
            {
                Name = jobDto.Name
            };

            await _unitOfWork.JobRepository.AddJobAsync(job);
        }

        public async Task UpdateJobAsync(JobUpdateDto jobDto)
        {
            var job = await _unitOfWork.JobRepository
                .GetJobByUuidAsync(jobDto.Uuid);

            if (job == null)
                throw new KeyNotFoundException("Job not found");

            job.Name = jobDto.Name;

            await _unitOfWork.JobRepository
                .UpdateJobAsync(job);
        }

        public async Task DeleteJobAsync(string uuid)
        {
            var job = await _unitOfWork.JobRepository
                .GetJobByUuidAsync(uuid);

            if (job == null)
                throw new KeyNotFoundException("Job not found");

            bool hasDoctors =
                await _unitOfWork.JobRepository
                    .HasDoctorsAsync(job.Id);

            if (hasDoctors)
            {
                throw new InvalidOperationException(
                    "Cannot delete this job because it has linked doctors."
                );
            }

            await _unitOfWork.JobRepository
                .DeleteJobAsync(uuid);
        }
    }
}
