using bootcamp_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_MVC_EF.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetCustomers()
        {
            IList<Customer> customers = new List<Customer>
            {
                new Customer { Id = 1,Name = "Khalid",Email = "Khalid@gmail.com",Phone = "0501234567"},
                new Customer {Id = 2,Name = "Afnan",Email = "Afnan@gmail.com",Phone = "0559876543"},
                new Customer {Id = 3,Name = "Abdulaziz",Email = "Abdulaziz@gmail.com",Phone = "0534567890"}
            };
            return Ok(customers);
        }
        public ActionResult GetCustomersFront()
        {
            IList<Customer> customers = new List<Customer>
            {
                new Customer { Id = 1,Name = "Khalid",Email = "Khalid@gmail.com",Phone = "0501234567"},
                new Customer {Id = 2,Name = "Afnan",Email = "Afnan@gmail.com",Phone = "0559876543"},
                new Customer {Id = 3,Name = "Abdulaziz",Email = "Abdulaziz@gmail.com",Phone = "0534567890"}
            };
            return View("index", customers);
        }
    }
}
