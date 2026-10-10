using ClinicManagementSystem.Data;
using ClinicManagementSystem.Dtos;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ClinicManagementSystem.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // =========================
        // Index
        // =========================
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsersAsync();

            var model = users.Select(u => new UserDto
            {
                Id = u.Id,
                Uuid = u.Uuid,
                Name = u.Name,
                UserName = u.UserName,
                Email = u.Email,
                IsLocked = u.IsLocked
            }).ToList();

            return View(model);
        }

        // =========================
        // Details
        // =========================
        [HttpGet]
        public async Task<IActionResult> Details(string uuid)
        {
            var user = await _userRepository.GetUserByUuidAsync(uuid);

            if (user == null)
            {
                return NotFound();
            }

            var model = new UserDto
            {
                Id = user.Id,
                Uuid = user.Uuid,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                IsLocked = user.IsLocked
            };

            return View(model);
        }

        // =========================
        // Create GET
        // =========================
        [HttpGet]
        public IActionResult Create()
        {
            return View(new UserCreateDto());
        }

        // =========================
        // Create POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateDto userDTO)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(userDTO.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email","This email is already registered.");
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = userDTO.Name,
                    UserName = userDTO.UserName,
                    Email = userDTO.Email,

                    Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),

                    IsLocked = userDTO.IsLocked
                };

                await _userRepository.AddUserAsync(user);

                return RedirectToAction(nameof(Index));
            }

            return View(userDTO);
        }

        // =========================
        // Edit GET
        // =========================
        [HttpGet]
        public async Task<IActionResult> Edit(string uuid)
        {
            var user = await _userRepository.GetUserByUuidAsync(uuid);

            if (user == null)
            {
                return NotFound();
            }

            var model = new UserUpdateDto
            {
                Uuid = user.Uuid,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                IsLocked = user.IsLocked
            };

            return View(model);
        }

        // =========================
        // Edit POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserUpdateDto userDTO)
        {
            var user = await _userRepository.GetUserByUuidAsync(userDTO.Uuid);

            if (user == null)
            {
                return NotFound();
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(userDTO.Email);

            if (existingUser != null && existingUser.Uuid != userDTO.Uuid)
            {
                ModelState.AddModelError("Email","This email is already registered.");
            }

            if (ModelState.IsValid)
            {
                user.Name = userDTO.Name;
                user.UserName = userDTO.UserName;
                user.Email = userDTO.Email;
                user.IsLocked = userDTO.IsLocked;

                if (!string.IsNullOrWhiteSpace(userDTO.Password))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
                }

                await _userRepository.UpdateUserAsync(user);

                return RedirectToAction(nameof(Index));
            }

            return View(userDTO);
        }

        // =========================
        // Delete GET
        // =========================
        [HttpGet]
        public async Task<IActionResult> Delete(string uuid)
        {
            var user = await _userRepository.GetUserByUuidAsync(uuid);

            if (user == null)
            {
                return NotFound();
            }

            var model = new UserDto
            {
                Id = user.Id,
                Uuid = user.Uuid,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                IsLocked = user.IsLocked
            };

            return View(model);
        }

        // =========================
        // Delete POST
        // =========================
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(string uuid)
        {
            var user = await _userRepository.GetUserByUuidAsync(uuid);

            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUserAsync(uuid);

            return RedirectToAction(nameof(Index));
        }
    }
}
