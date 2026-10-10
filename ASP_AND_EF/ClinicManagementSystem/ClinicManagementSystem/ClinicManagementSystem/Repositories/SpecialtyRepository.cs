using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Repositories
{
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly AppDbContext _context;

        public SpecialtyRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddSpecialtyAsync(Specialty specialty)
        {
            _context.Specialties.Add(specialty);
            return _context.SaveChangesAsync();
        }

        public Task DeleteSpecialtyAsync(string uuid)
        {
            var specialty = _context.Specialties
                .FirstOrDefault(s => s.Uuid == uuid);

            if (specialty != null)
            {
                _context.Specialties.Remove(specialty);
                return _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Specialty>>
            GetAllSpecialtiesAsync()
        {
            IEnumerable<Specialty> specialties =
                await _context.Specialties.ToListAsync();

            return specialties;
        }

        public async Task<Specialty?>
            GetSpecialtyByUuidAsync(string uuid)
        {
            var specialty =
                await _context.Specialties.FirstOrDefaultAsync(
                    s => s.Uuid == uuid
                );

            return specialty;
        }

        public Task UpdateSpecialtyAsync(Specialty specialty)
        {
            _context.Specialties.Update(specialty);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> HasDoctorsAsync(int specialtyId)
        {
            var doctor =
                await _context.Doctors.FirstOrDefaultAsync(
                    d => d.SpecialtyId == specialtyId
                );

            return doctor != null;
        }
    }
}
