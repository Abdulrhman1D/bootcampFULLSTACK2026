using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private readonly AppDbContext _db;
        public JobsController(AppDbContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            //Entity Framework Approach
            IEnumerable<Job> job = _db.Jobs.ToList();
            return View(job);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Job job)
        {
            if (ModelState.IsValid)
            {
                _db.Jobs.Add(job);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(job);
        }

        //===============
        //Edit
        //==========================
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var job = _db.Jobs.Find(Id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Job job)
        {
            if (ModelState.IsValid)
            {
                var oldJob = _db.Jobs.FirstOrDefault(
                    s => s.Id == job.Id
                );

                if (oldJob == null)
                {
                    return NotFound();
                }

                oldJob.Name = job.Name;

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(job);
        }

        // =========================
        // Delete GET
        // =========================
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var job = _db.Jobs.FirstOrDefault(
                s => s.Id == id
            );

            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // =========================
        // Delete POST
        // =========================
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var job = _db.Jobs.FirstOrDefault(
                j => j.Id == id
            );

            if (job == null)
            {
                return NotFound();
            }

            var doctor = _db.Doctors.FirstOrDefault(
                d => d.JobId == id
            );

            if (doctor != null)
            {
                ModelState.AddModelError(
                    "",
                    "Cannot delete this job because it has linked doctors."
                );

                return View("Delete", job);
            }

            _db.Jobs.Remove(job);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
