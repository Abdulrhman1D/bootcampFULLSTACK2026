using ClinicManagementSystem.Data;
using ClinicManagementSystem.Dtos;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository
            _appointmentRepository;

        public AppointmentsController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        // =========================
        // Index
        // =========================
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentRepository.GetAllAppointmentsAsync();

            var model = appointments.Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    Uuid = a.Uuid,

                    PatientName = a.Patient != null ? a.Patient.Name : "",

                    DoctorName = a.Doctor != null ? a.Doctor.Name : "",

                    AppointmentDate = a.AppointmentDate,
                    Status = a.Status,
                    Notes = a.Notes
                }).ToList();

            return View(model);
        }

        // =========================
        // Create GET
        // =========================
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadPatientsAndDoctorsAsync();

            return View(new AppointmentCreateDto());
        }

        // =========================
        // Create POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentCreateDto appointmentDTO)
        {
            if (ModelState.IsValid)
            {
                var appointment = new Appointment
                {
                    PatientId = appointmentDTO.PatientId,
                    DoctorId = appointmentDTO.DoctorId,
                    AppointmentDate = appointmentDTO.AppointmentDate,
                    Status = appointmentDTO.Status,
                    Notes = appointmentDTO.Notes
                };

                await _appointmentRepository.AddAppointmentAsync(appointment);

                return RedirectToAction(nameof(Index));
            }

            await LoadPatientsAndDoctorsAsync();

            return View(appointmentDTO);
        }

        // =========================
        // Edit GET
        // =========================
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            var appointment = await _appointmentRepository.GetAppointmentByUuidAsync(uuid);

            if (appointment == null)
            {
                return NotFound();
            }

            var model = new AppointmentUpdateDto
            {
                Uuid = appointment.Uuid,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
                Notes = appointment.Notes
            };

            await LoadPatientsAndDoctorsAsync();

            return View(model);
        }

        // =========================
        // Edit POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppointmentUpdateDto appointmentDTO)
        {
            if (ModelState.IsValid)
            {
                var appointment = await _appointmentRepository.GetAppointmentByUuidAsync(appointmentDTO.Uuid);

                if (appointment == null)
                {
                    return NotFound();
                }

                appointment.PatientId = appointmentDTO.PatientId;

                appointment.DoctorId = appointmentDTO.DoctorId;

                appointment.AppointmentDate = appointmentDTO.AppointmentDate;

                appointment.Status = appointmentDTO.Status;

                appointment.Notes = appointmentDTO.Notes;

                await _appointmentRepository.UpdateAppointmentAsync(appointment);

                return RedirectToAction(nameof(Index));
            }

            await LoadPatientsAndDoctorsAsync();

            return View(appointmentDTO);
        }

        // =========================
        // Delete GET
        // =========================
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            var appointment = await _appointmentRepository.GetAppointmentByUuidAsync(uuid);

            if (appointment == null)
            {
                return NotFound();
            }

            var model = new AppointmentDto
            {
                Id = appointment.Id,
                Uuid = appointment.Uuid,

                PatientName = appointment.Patient != null ? appointment.Patient.Name : "",

                DoctorName = appointment.Doctor != null ? appointment.Doctor.Name : "",

                AppointmentDate = appointment.AppointmentDate,

                Status = appointment.Status,
                Notes = appointment.Notes
            };

            return View(model);
        }

        // =========================
        // Delete POST
        // =========================
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(string uuid)
        {
            var appointment = await _appointmentRepository.GetAppointmentByUuidAsync(uuid);

            if (appointment == null)
            {
                return NotFound();
            }

            await _appointmentRepository.DeleteAppointmentAsync(uuid);

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // Load Lists
        // =========================
        private async Task LoadPatientsAndDoctorsAsync()
        {
            var patients = await _appointmentRepository.GetAllPatientsAsync();

            var doctors = await _appointmentRepository.GetAllDoctorsAsync();

            ViewBag.Patients = new SelectList(patients,"Id","Name");

            ViewBag.Doctors = new SelectList(doctors,"Id","Name");
        }
    }
}
