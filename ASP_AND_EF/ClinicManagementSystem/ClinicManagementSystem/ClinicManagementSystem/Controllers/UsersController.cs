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

    }
}
