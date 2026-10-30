using ClinicManagementSystem.Application.Dtos;
using ClinicManagementSystem.Domain.Models;
using ClinicManagementSystem.Infrastructure.Repositories.Base;
using ClinicManagementSystem.Services.Base;

namespace ClinicManagementSystem.Services
{
    public class SpecialtyServices : ISpecialtyServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpecialtyServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public async Task<SpecialtyDto?>
            GetSpecialtyDetailsByUuidAsync(string uuid)
        {
            var specialty =
                await _unitOfWork.SpecialtyRepository
                    .GetSpecialtyByUuidAsync(uuid);

            if (specialty == null)
                return null;

            return new SpecialtyDto
            {
                Id = specialty.Id,
                Uuid = specialty.Uuid,
                Name = specialty.Name
            };
        }

        public async Task<SpecialtyUpdateDto?>
            GetSpecialtyByUuidAsync(string uuid)
        {
            var specialty =
                await _unitOfWork.SpecialtyRepository
                    .GetSpecialtyByUuidAsync(uuid);

            if (specialty == null)
                return null;

            return new SpecialtyUpdateDto
            {
                Uuid = specialty.Uuid,
                Name = specialty.Name
            };
        }

        public async Task AddSpecialtyAsync(
            SpecialtyCreateDto specialtyDto)
        {
            var specialty = new Specialty
            {
                Name = specialtyDto.Name
            };

            await _unitOfWork.SpecialtyRepository
                .AddSpecialtyAsync(specialty);
        }

        public async Task UpdateSpecialtyAsync(
            SpecialtyUpdateDto specialtyDto)
        {
            var specialty =
                await _unitOfWork.SpecialtyRepository
                    .GetSpecialtyByUuidAsync(
                        specialtyDto.Uuid
                    );

            if (specialty == null)
            {
                throw new KeyNotFoundException(
                    "Specialty not found"
                );
            }

            specialty.Name = specialtyDto.Name;

            await _unitOfWork.SpecialtyRepository
                .UpdateSpecialtyAsync(specialty);
        }

        public async Task DeleteSpecialtyAsync(string uuid)
        {
            var specialty =
                await _unitOfWork.SpecialtyRepository
                    .GetSpecialtyByUuidAsync(uuid);

            if (specialty == null)
            {
                throw new KeyNotFoundException(
                    "Specialty not found"
                );
            }

            bool hasDoctors =
                await _unitOfWork.SpecialtyRepository
                    .HasDoctorsAsync(specialty.Id);

            if (hasDoctors)
            {
                throw new InvalidOperationException(
                    "Cannot delete this specialty because it has linked doctors."
                );
            }

            await _unitOfWork.SpecialtyRepository
                .DeleteSpecialtyAsync(uuid);
        }
    }
}
