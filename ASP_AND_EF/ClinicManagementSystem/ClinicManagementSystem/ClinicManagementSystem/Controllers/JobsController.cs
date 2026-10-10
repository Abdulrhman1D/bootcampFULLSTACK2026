using ClinicManagementSystem.Data;
using ClinicManagementSystem.Dtos;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private readonly IJobRepository _jobRepository;

        public JobsController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        // =========================
        // Index
        // =========================
        public async Task<IActionResult> Index()
        {
            var jobs = await _jobRepository.GetAllJobsAsync();

            var model = jobs.Select(j => new JobDto
            {
                Id = j.Id,
                Uuid = j.Uuid,
                Name = j.Name
            }).ToList();

            return View(model);
        }

        // =========================
        // Create GET
        // =========================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // =========================
        // Create POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            JobCreateDto jobCreateDto)
        {
            if (ModelState.IsValid)
            {
                var job = new Job
                {
                    Name = jobCreateDto.Name
                };

                await _jobRepository.AddJobAsync(job);

                return RedirectToAction("Index");
            }

            return View(jobCreateDto);
        }

        // =========================
        // Edit GET
        // =========================
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            var job =
                await _jobRepository.GetJobByUuidAsync(uuid);

            if (job == null)
            {
                return NotFound();
            }

            var model = new JobUpdateDto
            {
                Uuid = job.Uuid,
                Name = job.Name
            };

            return View(model);
        }

        // =========================
        // Edit POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            JobUpdateDto jobUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var job =
                    await _jobRepository.GetJobByUuidAsync(
                        jobUpdateDto.Uuid
                    );

                if (job == null)
                {
                    return NotFound();
                }

                job.Name = jobUpdateDto.Name;

                await _jobRepository.UpdateJobAsync(job);

                return RedirectToAction("Index");
            }

            return View(jobUpdateDto);
        }

        // =========================
        // Delete GET
        // =========================
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            var job =
                await _jobRepository.GetJobByUuidAsync(uuid);

            if (job == null)
            {
                return NotFound();
            }

            var model = new JobDto
            {
                Id = job.Id,
                Uuid = job.Uuid,
                Name = job.Name
            };

            return View(model);
        }

        // =========================
        // Delete POST
        // =========================
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(
            string uuid)
        {
            var job =
                await _jobRepository.GetJobByUuidAsync(uuid);

            if (job == null)
            {
                return NotFound();
            }

            bool hasDoctors =
                await _jobRepository.HasDoctorsAsync(job.Id);

            if (hasDoctors)
            {
                ModelState.AddModelError(
                    "",
                    "Cannot delete this job because it has linked doctors."
                );

                var model = new JobDto
                {
                    Id = job.Id,
                    Uuid = job.Uuid,
                    Name = job.Name
                };

                return View("Delete", model);
            }

            await _jobRepository.DeleteJobAsync(uuid);

            return RedirectToAction("Index");
        }
    }
}