using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
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
        // =========================
        // Create GET
        // =========================
        [HttpGet]
        public ActionResult Create()
        {
            var model = new AppointmentViewModel
            {
                Patients = _db.Patients.ToList(),
                Doctors = _db.Doctors.ToList()
            };

            return View(model);
        }
        // =========================
        // Create POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentViewModel model)
        {
            var patient = _db.Patients.FirstOrDefault(
                p => p.Id == model.Appointment.PatientId
            );

            if (patient == null)
            {
                ModelState.AddModelError(
                    "Appointment.PatientId",
                    "Please select a valid patient."
                );
            }

            var doctor = _db.Doctors.FirstOrDefault(
                d => d.Id == model.Appointment.DoctorId
            );

            if (doctor == null)
            {
                ModelState.AddModelError(
                    "Appointment.DoctorId",
                    "Please select a valid doctor."
                );
            }

            if (ModelState.IsValid)
            {
                var appointment = new Appointment
                {
                    PatientId = model.Appointment.PatientId,
                    DoctorId = model.Appointment.DoctorId,
                    AppointmentDate = model.Appointment.AppointmentDate,
                    Status = model.Appointment.Status,
                    Notes = model.Appointment.Notes
                };

                _db.Appointments.Add(appointment);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            model.Patients = _db.Patients.ToList();
            model.Doctors = _db.Doctors.ToList();

            return View(model);
        }

        // =========================
        // Edit GET
        // =========================
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var appointment = _db.Appointments.FirstOrDefault(
                a => a.Id == id
            );

            if (appointment == null)
            {
                return NotFound();
            }

            var model = new AppointmentViewModel
            {
                Appointment = appointment,
                Patients = _db.Patients.ToList(),
                Doctors = _db.Doctors.ToList()
            };

            return View(model);
        }
        // =========================
        // Edit POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AppointmentViewModel model)
        {
            var oldAppointment = _db.Appointments.FirstOrDefault(
                a => a.Id == model.Appointment.Id
            );

            if (oldAppointment == null)
            {
                return NotFound();
            }

            var patient = _db.Patients.FirstOrDefault(
                p => p.Id == model.Appointment.PatientId
            );

            if (patient == null)
            {
                ModelState.AddModelError(
                    "Appointment.PatientId",
                    "Please select a valid patient."
                );
            }

            var doctor = _db.Doctors.FirstOrDefault(
                d => d.Id == model.Appointment.DoctorId
            );

            if (doctor == null)
            {
                ModelState.AddModelError(
                    "Appointment.DoctorId",
                    "Please select a valid doctor."
                );
            }

            if (ModelState.IsValid)
            {
                oldAppointment.PatientId = model.Appointment.PatientId;
                oldAppointment.DoctorId = model.Appointment.DoctorId;
                oldAppointment.AppointmentDate = model.Appointment.AppointmentDate;
                oldAppointment.Status = model.Appointment.Status;
                oldAppointment.Notes = model.Appointment.Notes;

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            model.Patients = _db.Patients.ToList();
            model.Doctors = _db.Doctors.ToList();

            return View(model);
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
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var appointment = _db.Appointments.FirstOrDefault(
                a => a.Id == id
            );

            if (appointment == null)
            {
                return NotFound();
            }

            _db.Appointments.Remove(appointment);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
