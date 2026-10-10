using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>>
            GetAllDoctorsAsync()
        {
            IEnumerable<Doctor> doctors = await _context.Doctors.Include(d => d.Specialty).Include(d => d.Job).ToListAsync();

            return doctors;
        }

        public async Task<Doctor?>
            GetDoctorByUuidAsync(string uuid)
        {
            var doctor = await _context.Doctors.Include(d => d.Specialty).Include(d => d.Job).FirstOrDefaultAsync(d => d.Uuid == uuid);

            return doctor;
        }

        public Task AddDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);

            return _context.SaveChangesAsync();
        }

        public Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);

            return _context.SaveChangesAsync();
        }

        public Task DeleteDoctorAsync(string uuid)
        {
            var doctor = _context.Doctors.FirstOrDefault(
                d => d.Uuid == uuid
            );

            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);

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

        public async Task<IEnumerable<Job>>
            GetAllJobsAsync()
        {
            IEnumerable<Job> jobs =
                await _context.Jobs.ToListAsync();

            return jobs;
        }

        public async Task<bool>
            HasAppointmentsAsync(int doctorId)
        {
            var appointment =
                await _context.Appointments.FirstOrDefaultAsync(
                    a => a.DoctorId == doctorId
                );

            return appointment != null;
        }
    }
}
