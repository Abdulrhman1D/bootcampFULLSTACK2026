using ClinicManagementSystem.Application.Dtos;
using ClinicManagementSystem.Domain.Models;
using ClinicManagementSystem.Infrastructure.Repositories.Base;
using ClinicManagementSystem.Services.Base;

namespace ClinicManagementSystem.Services
{
    public class DoctorServices : IDoctorServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DoctorDto>>
            GetAllDoctorsAsync()
        {
            var doctors =
                await _unitOfWork.DoctorRepository
                    .GetAllDoctorsAsync();

            return doctors.Select(d => new DoctorDto
            {
                Id = d.Id,
                Uuid = d.Uuid,
                Name = d.Name,
                Phone = d.Phone,
                SpecialtyName = d.Specialty?.Name ?? string.Empty,
                JobName = d.Job?.Name ?? string.Empty
            });
        }

        public async Task<DoctorDto?>
            GetDoctorDetailsByUuidAsync(string uuid)
        {
            var doctor =
                await _unitOfWork.DoctorRepository
                    .GetDoctorByUuidAsync(uuid);

            if (doctor == null)
                return null;

            return new DoctorDto
            {
                Id = doctor.Id,
                Uuid = doctor.Uuid,
                Name = doctor.Name,
                Phone = doctor.Phone,
                SpecialtyName =
                    doctor.Specialty?.Name ?? string.Empty,
                JobName = doctor.Job?.Name ?? string.Empty
            };
        }

        public async Task<DoctorUpdateDto?>
            GetDoctorByUuidAsync(string uuid)
        {
            var doctor =
                await _unitOfWork.DoctorRepository
                    .GetDoctorByUuidAsync(uuid);

            if (doctor == null)
                return null;

            return new DoctorUpdateDto
            {
                Uuid = doctor.Uuid,
                Name = doctor.Name,
                Phone = doctor.Phone,
                SpecialtyId = doctor.SpecialtyId,
                JobId = doctor.JobId
            };
        }

        public async Task<IEnumerable<SpecialtyDto>>
            GetAllSpecialtiesAsync()
        {
            var specialties =
                await _unitOfWork.SpecialtyRepository
                    .GetAllSpecialtiesAsync();

            return specialties.Select(s => new SpecialtyDto
            {
                Id = s.Id,
                Uuid = s.Uuid,
                Name = s.Name
            });
        }

        public async Task<IEnumerable<JobDto>>
            GetAllJobsAsync()
        {
            var jobs =
                await _unitOfWork.JobRepository
                    .GetAllJobsAsync();

            return jobs.Select(j => new JobDto
            {
                Id = j.Id,
                Uuid = j.Uuid,
                Name = j.Name
            });
        }

        public async Task AddDoctorAsync(
            DoctorCreateDto doctorDto)
        {
            var doctor = new Doctor
            {
                Name = doctorDto.Name,
                Phone = doctorDto.Phone,
                SpecialtyId = doctorDto.SpecialtyId,
                JobId = doctorDto.JobId
            };

            await _unitOfWork.DoctorRepository
                .AddDoctorAsync(doctor);
        }

        public async Task UpdateDoctorAsync(
            DoctorUpdateDto doctorDto)
        {
            var doctor =
                await _unitOfWork.DoctorRepository
                    .GetDoctorByUuidAsync(doctorDto.Uuid);

            if (doctor == null)
                throw new KeyNotFoundException("Doctor not found");

            doctor.Name = doctorDto.Name;
            doctor.Phone = doctorDto.Phone;
            doctor.SpecialtyId = doctorDto.SpecialtyId;
            doctor.JobId = doctorDto.JobId;

            await _unitOfWork.DoctorRepository
                .UpdateDoctorAsync(doctor);
        }

        public async Task DeleteDoctorAsync(string uuid)
        {
            var doctor =
                await _unitOfWork.DoctorRepository
                    .GetDoctorByUuidAsync(uuid);

            if (doctor == null)
                throw new KeyNotFoundException("Doctor not found");

            bool hasAppointments =
                await _unitOfWork.DoctorRepository
                    .HasAppointmentsAsync(doctor.Id);

            if (hasAppointments)
            {
                throw new InvalidOperationException(
                    "Cannot delete this doctor because they have linked appointments."
                );
            }

            await _unitOfWork.DoctorRepository
                .DeleteDoctorAsync(uuid);
        }
    }
}
