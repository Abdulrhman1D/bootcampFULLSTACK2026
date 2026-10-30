using ClinicManagementSystem.Application.Dtos;
using ClinicManagementSystem.Domain.Models;
using ClinicManagementSystem.Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        //private readonly IPatientRepository _patientRepository;

        //public PatientsController(IPatientRepository patientRepository)
        //{
        //    _patientRepository = patientRepository;
        //}

        private readonly IUnitOfWork _unitOfWork;

        public PatientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        // =========================
        // Index
        // =========================
        public async Task<IActionResult> Index()
        {
            var patients = await _unitOfWork.PatientRepository.GetAllPatientsAsync();

            var model = patients.Select(p => new PatientDto
            {
                Id = p.Id,
                Uuid = p.Uuid,
                Name = p.Name,
                Phone = p.Phone,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender
            }).ToList();

            return View(model);
        }

        // =========================
        // Create GET
        // =========================
        [HttpGet]
        public IActionResult Create()
        {
            return View(new PatientCreateDto());
        }

        // =========================
        // Create POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientCreateDto patientDTO)
        {
            if (ModelState.IsValid)
            {
                var patient = new Patient
                {
                    Name = patientDTO.Name,
                    Phone = patientDTO.Phone,
                    DateOfBirth = patientDTO.DateOfBirth,
                    Gender = patientDTO.Gender
                };

                await _unitOfWork.PatientRepository.AddPatientAsync(patient);

                return RedirectToAction(nameof(Index));
            }

            return View(patientDTO);
        }

        // =========================
        // Edit GET
        // =========================
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            var patient = await _unitOfWork.PatientRepository.GetPatientByUuidAsync(uuid);

            if (patient == null)
            {
                return NotFound();
            }

            var model = new PatientUpdateDto
            {
                Uuid = patient.Uuid,
                Name = patient.Name,
                Phone = patient.Phone,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender
            };

            return View(model);
        }

        // =========================
        // Edit POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientUpdateDto patientDTO)
        {
            if (ModelState.IsValid)
            {
                var patient = await _unitOfWork.PatientRepository.GetPatientByUuidAsync(patientDTO.Uuid);

                if (patient == null)
                {
                    return NotFound();
                }

                patient.Name = patientDTO.Name;
                patient.Phone = patientDTO.Phone;
                patient.DateOfBirth = patientDTO.DateOfBirth;
                patient.Gender = patientDTO.Gender;

                await _unitOfWork.PatientRepository.UpdatePatientAsync(patient);

                return RedirectToAction(nameof(Index));
            }

            return View(patientDTO);
        }

        // =========================
        // Delete GET
        // =========================
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            var patient = await _unitOfWork.PatientRepository.GetPatientByUuidAsync(uuid);

            if (patient == null)
            {
                return NotFound();
            }

            var model = new PatientDto
            {
                Id = patient.Id,
                Uuid = patient.Uuid,
                Name = patient.Name,
                Phone = patient.Phone,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender
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
            var patient = await _unitOfWork.PatientRepository.GetPatientByUuidAsync(uuid);

            if (patient == null)
            {
                return NotFound();
            }

            bool hasAppointments = await _unitOfWork.PatientRepository.HasAppointmentsAsync(patient.Id);

            if (hasAppointments)
            {
                ModelState.AddModelError("","Cannot delete this patient because they have linked appointments.");

                var model = new PatientDto
                {
                    Id = patient.Id,
                    Uuid = patient.Uuid,
                    Name = patient.Name,
                    Phone = patient.Phone,
                    DateOfBirth = patient.DateOfBirth,
                    Gender = patient.Gender
                };

                return View("Delete", model);
            }

            await _unitOfWork.PatientRepository.DeletePatientAsync(uuid);

            return RedirectToAction(nameof(Index));
        }
    }
}
