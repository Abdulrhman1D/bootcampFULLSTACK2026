using ClinicManagementSystem.Application.Dtos;
using ClinicManagementSystem.Application.Services;
using ClinicManagementSystem.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDoctorServices _doctorServices ;
        private readonly IUserServices _userServices ;


        public UsersController(IDoctorServices employeeService, IUserServices userServices)
        {
            _doctorServices = employeeService;
            _userServices = userServices;
        }



        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _userServices.Login(loginDto);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            return Ok(new
            {

                message = "Login successful",
                id = user.Id,
                name = user.Name,
                username = user.UserName,
                email = user.Email,
            });
        }



        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            var user = _userServices.Register(registerDto);
            if (user == null)
            {
                return BadRequest(new { message = "Username or email already exists" });
            }
            return Ok(new { message = "Registration successful", user });
        }




        //[HttpGet("GetAllUsers")]
        //public IActionResult GetAllUsers()
        //{
        //    // Logic to retrieve all users from the database
        //    var users = new List<object>
        //    {
        //        new { Id = 1, Name = "Abdulrhman", Email = "Abdulrhman1aldossry@gmail.com" }
        //    };
        //    return Ok(new { message = "Get all users", users });
        //}


        //[HttpGet("GetAllEmployees")]
        //public async Task<IActionResult> GetAllEmployees()
        //{
        //    var doctors = await _doctorServices.GetAllDoctorsAsync();

        //    return Ok(new { message = "Get all employees", doctors });
        //}
    }
}
