using ClinicManagementSystem.Application.Dtos;

namespace ClinicManagementSystem.Services.Base
{
    public interface ISpecialtyServices
    {
        Task<IEnumerable<SpecialtyDto>> GetAllSpecialtiesAsync();
        Task<SpecialtyDto?> GetSpecialtyDetailsByUuidAsync(string uuid);
        Task<SpecialtyUpdateDto?> GetSpecialtyByUuidAsync(string uuid);

        Task AddSpecialtyAsync(SpecialtyCreateDto specialtyDto);
        Task UpdateSpecialtyAsync(SpecialtyUpdateDto specialtyDto);
        Task DeleteSpecialtyAsync(string uuid);
    }
}
