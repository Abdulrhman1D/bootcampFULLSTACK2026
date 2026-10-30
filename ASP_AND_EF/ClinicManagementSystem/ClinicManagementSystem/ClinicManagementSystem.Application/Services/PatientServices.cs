using ClinicManagementSystem.Application.Dtos;
using ClinicManagementSystem.Domain.Models;
using ClinicManagementSystem.Infrastructure.Repositories.Base;
using ClinicManagementSystem.Services.Base;

namespace ClinicManagementSystem.Services
{
    public class PatientServices : IPatientServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PatientDto>>
            GetAllPatientsAsync()
        {
            var patients =
                await _unitOfWork.PatientRepository
                    .GetAllPatientsAsync();

            return patients.Select(MapPatient);
        }

        public async Task<PatientDto?>
            GetPatientDetailsByUuidAsync(string uuid)
        {
            var patient =
                await _unitOfWork.PatientRepository
                    .GetPatientByUuidAsync(uuid);

            return patient == null ? null : MapPatient(patient);
        }

        public async Task<PatientUpdateDto?>
            GetPatientByUuidAsync(string uuid)
        {
            var patient =
                await _unitOfWork.PatientRepository
                    .GetPatientByUuidAsync(uuid);

            if (patient == null)
                return null;

            return new PatientUpdateDto
            {
                Uuid = patient.Uuid,
                Name = patient.Name,
                Phone = patient.Phone,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender
            };
        }

        public async Task AddPatientAsync(
            PatientCreateDto patientDto)
        {
            var patient = new Patient
            {
                Name = patientDto.Name,
                Phone = patientDto.Phone,
                DateOfBirth = patientDto.DateOfBirth,
                Gender = patientDto.Gender
            };

            await _unitOfWork.PatientRepository
                .AddPatientAsync(patient);
        }

        public async Task UpdatePatientAsync(
            PatientUpdateDto patientDto)
        {
            var patient =
                await _unitOfWork.PatientRepository
                    .GetPatientByUuidAsync(patientDto.Uuid);

            if (patient == null)
                throw new KeyNotFoundException("Patient not found");

            patient.Name = patientDto.Name;
            patient.Phone = patientDto.Phone;
            patient.DateOfBirth = patientDto.DateOfBirth;
            patient.Gender = patientDto.Gender;

            await _unitOfWork.PatientRepository
                .UpdatePatientAsync(patient);
        }

        public async Task DeletePatientAsync(string uuid)
        {
            var patient =
                await _unitOfWork.PatientRepository
                    .GetPatientByUuidAsync(uuid);

            if (patient == null)
                throw new KeyNotFoundException("Patient not found");

            bool hasAppointments =
                await _unitOfWork.PatientRepository
                    .HasAppointmentsAsync(patient.Id);

            if (hasAppointments)
            {
                throw new InvalidOperationException(
                    "Cannot delete this patient because they have linked appointments."
                );
            }

            await _unitOfWork.PatientRepository
                .DeletePatientAsync(uuid);
        }

        private static PatientDto MapPatient(Patient patient)
        {
            return new PatientDto
            {
                Id = patient.Id,
                Uuid = patient.Uuid,
                Name = patient.Name,
                Phone = patient.Phone,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender
            };
        }
    }
}
