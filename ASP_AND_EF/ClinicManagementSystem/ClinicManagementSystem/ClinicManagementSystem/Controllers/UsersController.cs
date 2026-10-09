using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ClinicManagementSystem.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {

        private readonly AppDbContext _db;

        public UsersController(AppDbContext db)
        {
            _db = db;
        }

        // =========================
        // Index
        // =========================
        public IActionResult Index()
        {
            var users = _db.Users.ToList();

            return View(users);
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
        public IActionResult Create(User user)
        {
            var existingUser = _db.Users.FirstOrDefault(
                u => u.Email == user.Email
            );

            if (existingUser != null)
            {
                ModelState.AddModelError(
                    "Email",
                    "This email is already registered."
                );
            }

            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    Name = user.Name,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                    IsLocked = user.IsLocked
                };

                _db.Users.Add(newUser);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }
        // =========================
        // Edit GET
        // =========================
        public IActionResult Edit(int id)
        {
            var user = _db.Users.Find(id);

            if (user == null)
                return NotFound();

            return View(user);
        }


        // =========================
        // Edit POST
        // =========================
        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var oldUser = _db.Users.Find(user.Id);

                if (oldUser == null)
                    return NotFound();

                oldUser.Name = user.Name;
                oldUser.UserName = user.UserName;
                oldUser.Email = user.Email;
                oldUser.IsLocked = user.IsLocked;


                if (!string.IsNullOrEmpty(user.Password))
                {

                    oldUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }

        // =========================
        // Delete GET
        // =========================
        public IActionResult Delete(int id)
        {
            var user = _db.Users.Find(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var user = _db.Users.Find(id);

            if (user == null)
                return NotFound();

            _db.Users.Remove(user);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
