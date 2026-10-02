using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        private readonly AppDbContext _db;
        public DoctorsController(AppDbContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            //Entity Framework Approach
            //IEnumerable<Doctor> Doctors = _db.Doctors.ToList();
            var doctors = _db.Doctors
            .Include(d => d.Specialty)
            .ToList();
            return View(doctors);
        }
        // =========================
        // Create GET
        // =========================
        [HttpGet]
        public ActionResult Create()
        {
            var model = new DoctorViewModel
            {
                Specialties = _db.Specialties.ToList()
            };

            return View(model);
        }
        // =========================
        // Create POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorViewModel model)
        {
            var specialty = _db.Specialties.FirstOrDefault(
                s => s.Id == model.Doctor.SpecialtyId
                );

            if (specialty == null)
            {
                ModelState.AddModelError(
                    "Doctor.SpecialtyId",
                    "Please select a valid specialty."
                );
            }

            if (ModelState.IsValid)
            {
                var doctor = new Doctor
                {
                    Name = model.Doctor.Name,
                    Phone = model.Doctor.Phone,
                    SpecialtyId = model.Doctor.SpecialtyId
                };

                _db.Doctors.Add(doctor);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            model.Specialties = _db.Specialties.ToList();

            return View(model);
        }
        // =========================
        // Edit GET
        // =========================
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var doctor = _db.Doctors.Find(id);

            if (doctor == null)
            {
                return NotFound();
            }

            var model = new DoctorViewModel
            {
                Doctor = doctor,
                Specialties = _db.Specialties.ToList()
            };

            return View(model);
        }
        // =========================
        // Edit POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DoctorViewModel model)
        {
            var oldDoctor = _db.Doctors.Find(model.Doctor.Id);

            if (oldDoctor == null)
            {
                return NotFound();
            }

            var specialty = _db.Specialties.FirstOrDefault(
                s => s.Id == model.Doctor.SpecialtyId
                );

            if (specialty == null)
            {
                ModelState.AddModelError(
                    "Doctor.SpecialtyId",
                    "Please select a valid specialty."
                );
            }

            if (ModelState.IsValid)
            {
                oldDoctor.Name = model.Doctor.Name;
                oldDoctor.Phone = model.Doctor.Phone;
                oldDoctor.SpecialtyId = model.Doctor.SpecialtyId;

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            model.Specialties = _db.Specialties.ToList();

            return View(model);
        }
        // =========================
        // Delete GET
        // =========================
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var doctor = _db.Doctors
                .Include(d => d.Specialty)
                .FirstOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // =========================
        // Delete POST
        // =========================
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var doctor = _db.Doctors
                .Include(d => d.Specialty)
                .FirstOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            var appointment = _db.Appointments.FirstOrDefault(
                a => a.DoctorId == id
            );

            if (appointment != null)
            {
                ModelState.AddModelError(
                    "",
                    "Cannot delete this doctor because they have linked appointments."
                );

                return View("Delete", doctor);
            }

            _db.Doctors.Remove(doctor);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
