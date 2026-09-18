using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Controllers
{
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
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Specialties = new SelectList(
                _db.Specialties.ToList(),
                "Id","Name"
            );
            return View();
        }
        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _db.Doctors.Add(doctor);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Specialties = new SelectList(
                _db.Specialties.ToList(),
                "Id","Name"
                );

            return View(doctor);
        }
        //===============
        //Edit
        //==========================
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var doctor = _db.Doctors.Find(Id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewBag.Specialties = new SelectList(
                _db.Specialties.ToList(),
                "Id",
                "Name",
                doctor.SpecialtyId
            );
            return View(doctor);
        }
        [HttpPost]
        public ActionResult Edit(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _db.Doctors.Update(doctor);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Specialties = new SelectList(
                _db.Specialties.ToList(),
                "Id",
                "Name",
                doctor.SpecialtyId
            );
            return View(doctor);
        }
        //===============
        //Delete
        //==========================
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var doctor = _db.Doctors.Find(Id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }
        [HttpPost]
        public ActionResult Delete(Doctor doctor)
        {
            _db.Doctors.Remove(doctor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
