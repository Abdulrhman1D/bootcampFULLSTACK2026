using ClinicManagementSystem.Data;
using ClinicManagementSystem.Dtos;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ClinicManagementSystem.Controllers
{
    [Authorize]
    public class SpecialtiesController : Controller
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public SpecialtiesController(
            ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        public async Task<IActionResult> Index()
        {
            var specialties =
                await _specialtyRepository.GetAllSpecialtiesAsync();

            var model = specialties.Select(s => new SpecialtyDto
            {
                Id = s.Id,
                Uuid = s.Uuid,
                Name = s.Name
            }).ToList();

            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialtyCreateDto specialtyCreateDto)
        {
            if (ModelState.IsValid)
            {
                var specialty = new Specialty
                {
                    Name = specialtyCreateDto.Name
                };

                await _specialtyRepository.AddSpecialtyAsync(specialty);

                return RedirectToAction("Index");
            }

            return View(specialtyCreateDto);
        }

        //===============
        //Edit
        //==========================
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            var specialty =
                await _specialtyRepository.GetSpecialtyByUuidAsync(uuid);

            if (specialty == null)
            {
                return NotFound();
            }

            var model = new SpecialtyUpdateDto
            {
                Uuid = specialty.Uuid,
                Name = specialty.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialtyUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var specialty =
                    await _specialtyRepository.GetSpecialtyByUuidAsync(
                        model.Uuid
                    );

                if (specialty == null)
                {
                    return NotFound();
                }

                specialty.Name = model.Name;

                await _specialtyRepository.UpdateSpecialtyAsync(specialty);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // =========================
        // Delete GET
        // =========================
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            var specialty =
                await _specialtyRepository.GetSpecialtyByUuidAsync(uuid);

            if (specialty == null)
            {
                return NotFound();
            }

            var model = new SpecialtyDto
            {
                Id = specialty.Id,
                Uuid = specialty.Uuid,
                Name = specialty.Name
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
            var specialty =
                await _specialtyRepository.GetSpecialtyByUuidAsync(uuid);

            if (specialty == null)
            {
                return NotFound();
            }

            bool hasDoctors =
                await _specialtyRepository.HasDoctorsAsync(specialty.Id);

            if (hasDoctors)
            {
                ModelState.AddModelError(
                    "",
                    "Cannot delete this specialty because it has linked doctors."
                );

                var model = new SpecialtyDto
                {
                    Id = specialty.Id,
                    Uuid = specialty.Uuid,
                    Name = specialty.Name
                };

                return View("Delete", model);
            }

            await _specialtyRepository.DeleteSpecialtyAsync(uuid);

            return RedirectToAction("Index");
        }
    }
}
