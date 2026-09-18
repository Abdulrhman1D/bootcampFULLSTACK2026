using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _db;
        public AppointmentsController(AppDbContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            var appointments = _db.Appointments.Include(a => a.Patient).Include(a => a.Doctor).ToList();
        
            return View(appointments);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Patients = new SelectList(_db.Patients.ToList(), "Id", "Name");
            ViewBag.Doctors = new SelectList(_db.Doctors.ToList(), "Id", "Name");

            return View();
        }
        [HttpPost]
        public ActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _db.Appointments.Add(appointment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Patients = new SelectList(_db.Patients.ToList(), "Id", "Name");
            ViewBag.Doctors = new SelectList(_db.Doctors.ToList(), "Id", "Name");
            return View(appointment);
        }
        //===============
        //Edit
        //==========================
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var appointment = _db.Appointments.Find(Id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewBag.Patients = new SelectList(
                _db.Patients.ToList(),
                "Id",
                "Name",
                appointment.PatientId
            );

            ViewBag.Doctors = new SelectList(
                _db.Doctors.ToList(),
                "Id",
                "Name",
                appointment.DoctorId
            );
            return View(appointment);
        }
        [HttpPost]
        public ActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _db.Appointments.Update(appointment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(appointment);
        }
        //==========================
        //Delete
        //==========================
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var appointment = _db.Appointments.Include(a => a.Patient).Include(a => a.Doctor).FirstOrDefault(a => a.Id == Id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }
        [HttpPost]
        public ActionResult Delete(Appointment appointment)
        {
            _db.Appointments.Remove(appointment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
