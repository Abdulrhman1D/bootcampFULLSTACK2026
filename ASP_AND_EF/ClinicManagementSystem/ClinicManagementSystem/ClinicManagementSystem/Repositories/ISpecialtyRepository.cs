using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync();

        Task<Specialty?> GetSpecialtyByUuidAsync(string uuid);

        Task AddSpecialtyAsync(Specialty specialty);

        Task UpdateSpecialtyAsync(Specialty specialty);

        Task DeleteSpecialtyAsync(string uuid);

        Task<bool> HasDoctorsAsync(int specialtyId);
    }
}
