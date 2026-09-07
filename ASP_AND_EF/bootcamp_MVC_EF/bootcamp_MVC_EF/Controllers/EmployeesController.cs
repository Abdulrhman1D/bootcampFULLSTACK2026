using bootcamp_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_MVC_EF.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GetEmployees()
        {
            IList<Employee> employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Abdulrhman", Position = "Developer", Salary = 60000 },
                new Employee { Id = 2, Name = "Ghouson", Position = "Manager", Salary = 80000 },
                new Employee { Id = 3, Name = "ALanoud", Position = "Tester", Salary = 50000 }
            };

            return Ok(employees);
        }

        public ActionResult GetEmployeesFront()
        {
            IList<Employee> employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Abdulrhman", Position = "Developer", Salary = 60000 },
                new Employee { Id = 2, Name = "Ghouson", Position = "Manager", Salary = 80000 },
                new Employee { Id = 3, Name = "ALanoud", Position = "Tester", Salary = 50000 }
            };

            return View("index",employees);
        }
    }
}
