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
    public class DoctorsController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorsController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        // =========================
        // Index
        // =========================
        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorRepository.GetAllDoctorsAsync();

            var model = doctors.Select(d => new DoctorDto
            {
                Id = d.Id,
                Uuid = d.Uuid,
                Name = d.Name,
                Phone = d.Phone,

                SpecialtyName = d.Specialty != null? d.Specialty.Name: "",

                JobName = d.Job != null? d.Job.Name: ""}).ToList();

            return View(model);
        }

        // =========================
        // Create GET
        // =========================
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadSpecialtiesAndJobsAsync();

            return View(new DoctorCreateDto());
        }

        // =========================
        // Create POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorCreateDto doctorDTO)
        {
            if (ModelState.IsValid)
            {
                var doctor = new Doctor
                {
                    Name = doctorDTO.Name,
                    Phone = doctorDTO.Phone,
                    SpecialtyId = doctorDTO.SpecialtyId,
                    JobId = doctorDTO.JobId
                };

                await _doctorRepository.AddDoctorAsync(doctor);

                return RedirectToAction(nameof(Index));
            }

            await LoadSpecialtiesAndJobsAsync();

            return View(doctorDTO);
        }

        // =========================
        // Edit GET
        // =========================
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            var doctor = await _doctorRepository.GetDoctorByUuidAsync(uuid);

            if (doctor == null)
            {
                return NotFound();
            }

            var model = new DoctorUpdateDto
            {
                Uuid = doctor.Uuid,
                Name = doctor.Name,
                Phone = doctor.Phone,
                SpecialtyId = doctor.SpecialtyId,
                JobId = doctor.JobId
            };

            await LoadSpecialtiesAndJobsAsync();

            return View(model);
        }

        // =========================
        // Edit POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorUpdateDto doctorDTO)
        {
            if (ModelState.IsValid)
            {
                var doctor = await _doctorRepository.GetDoctorByUuidAsync(doctorDTO.Uuid);

                if (doctor == null)
                {
                    return NotFound();
                }

                doctor.Name = doctorDTO.Name;
                doctor.Phone = doctorDTO.Phone;
                doctor.SpecialtyId = doctorDTO.SpecialtyId;
                doctor.JobId = doctorDTO.JobId;

                await _doctorRepository.UpdateDoctorAsync(doctor);

                return RedirectToAction(nameof(Index));
            }

            await LoadSpecialtiesAndJobsAsync();

            return View(doctorDTO);
        }

        // =========================
        // Delete GET
        // =========================
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            var doctor = await _doctorRepository.GetDoctorByUuidAsync(uuid);

            if (doctor == null)
            {
                return NotFound();
            }

            var model = new DoctorDto
            {
                Id = doctor.Id,
                Uuid = doctor.Uuid,
                Name = doctor.Name,
                Phone = doctor.Phone,

                SpecialtyName = doctor.Specialty != null? doctor.Specialty.Name: "",

                JobName = doctor.Job != null? doctor.Job.Name: ""
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
            var doctor = await _doctorRepository.GetDoctorByUuidAsync(uuid);

            if (doctor == null)
            {
                return NotFound();
            }

            bool hasAppointments = await _doctorRepository.HasAppointmentsAsync(doctor.Id);

            if (hasAppointments)
            {
                ModelState.AddModelError("","Cannot delete this doctor because they have linked appointments.");

                var model = new DoctorDto
                {
                    Id = doctor.Id,
                    Uuid = doctor.Uuid,
                    Name = doctor.Name,
                    Phone = doctor.Phone,

                    SpecialtyName = doctor.Specialty != null? doctor.Specialty.Name: "",

                    JobName = doctor.Job != null? doctor.Job.Name: ""
                };

                return View("Delete", model);
            }

            await _doctorRepository.DeleteDoctorAsync(uuid);

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // Load Lists
        // =========================
        private async Task LoadSpecialtiesAndJobsAsync()
        {
            var specialties = await _doctorRepository.GetAllSpecialtiesAsync();

            var jobs = await _doctorRepository.GetAllJobsAsync();

            ViewBag.Specialties = new SelectList(specialties,"Id","Name");

            ViewBag.Jobs = new SelectList(jobs,"Id","Name");
        }
    }
}
