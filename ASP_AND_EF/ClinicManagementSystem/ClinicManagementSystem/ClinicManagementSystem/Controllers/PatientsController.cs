using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly AppDbContext _db;
        public PatientsController(AppDbContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            //Entity Framework Approach
            IEnumerable<Patient> patients = _db.Patients.ToList();
            return View(patients);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _db.Patients.Add(patient);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(patient);
        }
        //===============
        //Edit
        //==========================
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var patient = _db.Patients.Find(Id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var oldPatient = _db.Patients.FirstOrDefault(
                    p => p.Id == patient.Id
                );

                if (oldPatient == null)
                {
                    return NotFound();
                }

                oldPatient.Name = patient.Name;
                oldPatient.Phone = patient.Phone;
                oldPatient.DateOfBirth = patient.DateOfBirth;
                oldPatient.Gender = patient.Gender;

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(patient);
        }
        //===============
        //Delete
        //==========================
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var patient = _db.Patients.Find(Id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var patient = _db.Patients.FirstOrDefault(
                p => p.Id == id
            );

            if (patient == null)
            {
                return NotFound();
            }

            var appointment = _db.Appointments.FirstOrDefault(
                a => a.PatientId == id
            );

            if (appointment != null)
            {
                ModelState.AddModelError(
                    "",
                    "Cannot delete this patient because they have linked appointments."
                );

                return View("Delete", patient);
            }

            _db.Patients.Remove(patient);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
