using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ClinicManagementSystem.Controllers
{
    [Authorize]
    public class SpecialtiesController : Controller
    {
        private readonly AppDbContext _db;
        public SpecialtiesController(AppDbContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            //Entity Framework Approach
            IEnumerable<Specialty> specialty = _db.Specialties.ToList();
            return View(specialty);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                _db.Specialties.Add(specialty);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(specialty);
        }

        //===============
        //Edit
        //==========================
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var specialty = _db.Specialties.Find(Id);
            if (specialty == null)
            {
                return NotFound();
            }
            return View(specialty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                var oldSpecialty = _db.Specialties.FirstOrDefault(
                    s => s.Id == specialty.Id
                );

                if (oldSpecialty == null)
                {
                    return NotFound();
                }

                oldSpecialty.Name = specialty.Name;

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(specialty);
        }

        // =========================
        // Delete GET
        // =========================
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var specialty = _db.Specialties.FirstOrDefault(
                s => s.Id == id
            );

            if (specialty == null)
            {
                return NotFound();
            }

            return View(specialty);
        }

        // =========================
        // Delete POST
        // =========================
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var specialty = _db.Specialties.FirstOrDefault(
                s => s.Id == id
            );

            if (specialty == null)
            {
                return NotFound();
            }

            var doctor = _db.Doctors.FirstOrDefault(
                d => d.SpecialtyId == id
            );

            if (doctor != null)
            {
                ModelState.AddModelError(
                    "",
                    "Cannot delete this specialty because it has linked doctors."
                );

                return View("Delete", specialty);
            }

            _db.Specialties.Remove(specialty);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
