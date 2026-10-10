using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>>GetAllAppointmentsAsync()
        {
            IEnumerable<Appointment> appointments =
                await _context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                    .ToListAsync();

            return appointments;
        }

        public async Task<Appointment?>
            GetAppointmentByUuidAsync(string uuid)
        {
            var appointment =
                await _context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                    .FirstOrDefaultAsync(
                        a => a.Uuid == uuid
                    );

            return appointment;
        }

        public Task AddAppointmentAsync(
            Appointment appointment)
        {
            _context.Appointments.Add(appointment);

            return _context.SaveChangesAsync();
        }

        public Task UpdateAppointmentAsync(
            Appointment appointment)
        {
            _context.Appointments.Update(appointment);

            return _context.SaveChangesAsync();
        }

        public Task DeleteAppointmentAsync(string uuid)
        {
            var appointment =
                _context.Appointments.FirstOrDefault(
                    a => a.Uuid == uuid
                );

            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);

                return _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Patient>>
            GetAllPatientsAsync()
        {
            IEnumerable<Patient> patients =
                await _context.Patients.ToListAsync();

            return patients;
        }

        public async Task<IEnumerable<Doctor>>
            GetAllDoctorsAsync()
        {
            IEnumerable<Doctor> doctors =
                await _context.Doctors.ToListAsync();

            return doctors;
        }
    }
}
