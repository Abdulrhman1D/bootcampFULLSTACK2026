using ClinicManagementSystem.Application.Dtos;
using ClinicManagementSystem.Domain.Models;
using ClinicManagementSystem.Infrastructure.Repositories.Base;
using ClinicManagementSystem.Services.Base;

namespace ClinicManagementSystem.Services
{
    public class AppointmentServices : IAppointmentServices
    {
        //DI "Dependency Injection"
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // CREATE
        public async Task AddAppointmentAsync(AppointmentCreateDto appointmentDto)
        {
            var appointment = new Appointment
            {
                PatientId = appointmentDto.PatientId,
                DoctorId = appointmentDto.DoctorId,
                AppointmentDate = appointmentDto.AppointmentDate,
                Status = appointmentDto.Status,
                Notes = appointmentDto.Notes
            };

            await _unitOfWork.AppointmentRepository.AddAppointmentAsync(appointment);
        }

        // DELETE
        public async Task DeleteAppointmentAsync(string uuid)
        {
            var employee = await _unitOfWork.AppointmentRepository.GetAppointmentByUuidAsync(uuid);

            if (employee == null)
            {
                throw new KeyNotFoundException("Appointment not found");
            }

            await _unitOfWork.AppointmentRepository.DeleteAppointmentAsync(uuid);
        }

        // GET ALL
        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsAsync();

            return appointments.Select(a =>new AppointmentDto
                {
                    Id = a.Id,
                    Uuid = a.Uuid,

                    PatientName = a.Patient != null ? a.Patient.Name : "",

                    DoctorName = a.Doctor != null ? a.Doctor.Name : "",

                    AppointmentDate = a.AppointmentDate,
                    Status = a.Status,
                    Notes = a.Notes
                });
        }

        //Details 

        public async Task<AppointmentDto?> GetAppointmentDetailsByUuidAsync(string uuid)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetAppointmentByUuidAsync(uuid);

            if (appointment == null)
            {
                return null;
            }

            return new AppointmentDto
            {
                Id = appointment.Id,
                Uuid = appointment.Uuid,

                PatientName = appointment.Patient?.Name ?? string.Empty,

                DoctorName = appointment.Doctor?.Name ?? string.Empty,

                AppointmentDate = appointment.AppointmentDate,

                Status = appointment.Status,
                Notes = appointment.Notes
            };
        }

        // GET ALL DOCTORS
        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _unitOfWork.DoctorRepository.GetAllDoctorsAsync();
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

        // GET ALL PATIENTS
        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _unitOfWork.PatientRepository.GetAllPatientsAsync();
            return patients.Select(p => new PatientDto
            {
                Id = p.Id,
                Uuid = p.Uuid,
                Name = p.Name,
                Phone = p.Phone,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender
            });
        }

        // GET Appointment BY Uuid 
        public async Task<AppointmentUpdateDto?> GetAppointmentByUuidAsync(string uuid)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetAppointmentByUuidAsync(uuid);

            if (appointment == null)
            {
                return null;
            }

            return new AppointmentUpdateDto
            {
                Uuid = appointment.Uuid,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
                Notes = appointment.Notes
            };
        }

        // UPDATE Appointment
        public async Task UpdateAppointmentAsync(AppointmentUpdateDto appointmentDto)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetAppointmentByUuidAsync(appointmentDto.Uuid);

            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found");
            }

            appointment.PatientId = appointmentDto.PatientId;

            appointment.DoctorId = appointmentDto.DoctorId;

            appointment.AppointmentDate = appointmentDto.AppointmentDate;

            appointment.Status = appointmentDto.Status;

            appointment.Notes = appointmentDto.Notes;

            await _unitOfWork.AppointmentRepository.UpdateAppointmentAsync(appointment);
        }
    }
}
