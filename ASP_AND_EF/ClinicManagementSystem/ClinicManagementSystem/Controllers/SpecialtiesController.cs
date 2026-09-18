using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
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
        public ActionResult Edit(Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                _db.Specialties.Update(specialty);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(specialty);
        }
        //===============
        //Delete
        //==========================
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var specialty = _db.Specialties.Find(Id);
            if (specialty == null)
            {
                return NotFound();
            }
            return View(specialty);
        }
        [HttpPost]
        public ActionResult Delete(Specialty specialty)
        {
            _db.Specialties.Remove(specialty);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
