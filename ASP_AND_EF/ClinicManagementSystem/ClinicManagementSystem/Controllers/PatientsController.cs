using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
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
        public ActionResult Create(Patient patient)
        {
            _db.Patients.Add(patient);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
        public ActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _db.Patients.Update(patient);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
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
        public ActionResult Delete(Patient patient)
        {
            _db.Patients.Remove(patient);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
