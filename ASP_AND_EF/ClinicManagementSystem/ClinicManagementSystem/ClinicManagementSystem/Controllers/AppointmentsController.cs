using ClinicManagementSystem.Application.Dtos;
using ClinicManagementSystem.Domain.Models;
using ClinicManagementSystem.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ClinicManagementSystem.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        //private readonly IAppointmentRepository
        //    _appointmentRepository;

        //public AppointmentsController(IAppointmentRepository appointmentRepository)
        //{
        //    _appointmentRepository = appointmentRepository;
        //}


        //private readonly IUnitOfWork _unitOfWork;

        //public AppointmentsController(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;

        //}

        private readonly IAppointmentServices _appointmentsService;

        public AppointmentsController(IAppointmentServices appointmentsService)
        {
            _appointmentsService = appointmentsService;
        }
        // =========================
        // Index
        // =========================
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentsService.GetAllAppointmentsAsync();

            return View(appointments);
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

                await _appointmentsService.AddAppointmentAsync(appointmentDTO);

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
            var appointment = await _appointmentsService.GetAppointmentByUuidAsync(uuid);

            if (appointment == null)
            {
                return NotFound();
            }

            await LoadPatientsAndDoctorsAsync();

            return View(appointment);
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
                var appointment = await _appointmentsService.GetAppointmentByUuidAsync(appointmentDTO.Uuid);

                if (appointment == null)
                {
                    return NotFound();
                }

                appointment.PatientId = appointmentDTO.PatientId;

                appointment.DoctorId = appointmentDTO.DoctorId;

                appointment.AppointmentDate = appointmentDTO.AppointmentDate;

                appointment.Status = appointmentDTO.Status;

                appointment.Notes = appointmentDTO.Notes;

                await _appointmentsService.UpdateAppointmentAsync(appointment);

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
            var appointment = await _appointmentsService.GetAppointmentDetailsByUuidAsync(uuid);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // =========================
        // Delete POST
        // =========================
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(string uuid)
        {
            var appointment = await _appointmentsService.GetAppointmentDetailsByUuidAsync(uuid);

            if (appointment == null)
            {
                return NotFound();
            }

            await _appointmentsService.DeleteAppointmentAsync(uuid);

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // Load Lists
        // =========================
        private async Task LoadPatientsAndDoctorsAsync()
        {
            var patients = await _appointmentsService.GetAllPatientsAsync();

            var doctors = await _appointmentsService.GetAllDoctorsAsync();

            ViewBag.Patients = new SelectList(patients,"Id","Name");

            ViewBag.Doctors = new SelectList(doctors,"Id","Name");
        }
    }
}
