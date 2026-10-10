using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>>
            GetAllPatientsAsync()
        {
            IEnumerable<Patient> patients =
                await _context.Patients.ToListAsync();

            return patients;
        }

        public async Task<Patient?>
            GetPatientByUuidAsync(string uuid)
        {
            var patient =
                await _context.Patients.FirstOrDefaultAsync(
                    p => p.Uuid == uuid
                );

            return patient;
        }

        public Task AddPatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);

            return _context.SaveChangesAsync();
        }

        public Task UpdatePatientAsync(Patient patient)
        {
            _context.Patients.Update(patient);

            return _context.SaveChangesAsync();
        }

        public Task DeletePatientAsync(string uuid)
        {
            var patient = _context.Patients.FirstOrDefault(
                p => p.Uuid == uuid
            );

            if (patient != null)
            {
                _context.Patients.Remove(patient);

                return _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }

        public async Task<bool>
            HasAppointmentsAsync(int patientId)
        {
            var appointment =
                await _context.Appointments.FirstOrDefaultAsync(
                    a => a.PatientId == patientId
                );

            return appointment != null;
        }
    }
}
