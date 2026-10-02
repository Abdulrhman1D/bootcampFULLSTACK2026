using ClinicManagementSystem.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClinicManagementSystem.Controllers
{
    public class AccountsController : Controller
    {

        private readonly AppDbContext _db;

        public AccountsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginConfirm(
            string email,
            string password)
        {
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(
                    "",
                    "Please enter your email and password."
                );

                return View("Login");
            }

            var user = _db.Users.FirstOrDefault(
                u => u.Email == email
            );

            if (user == null)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid email or password."
                );

                return View("Login");
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(
                password,
                user.Password
            );

            if (!isPasswordCorrect)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid email or password."
                );

                return View("Login");
            }

            if (user.IsLocked)
            {
                ModelState.AddModelError(
                    "",
                    "Your account is locked."
                );

                return View("Login");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()
                )
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction("Login");
        }

    }
}
