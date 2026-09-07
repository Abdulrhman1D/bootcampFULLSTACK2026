using bootcamp_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_MVC_EF.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetProducts()
        {
            IList<Product> products = new List<Product>
            {
                new Product {Id = 1, Name = "MacBook Air", Price = 4500, Category = "Laptops"},
                new Product {Id = 2, Name = "iPhone 12", Price = 3200, Category = "Mobiles"},
                new Product {Id = 3, Name = "Wireless Mouse", Price = 120, Category = "Accessories"}
            };
            return Ok(products);
        }
        public ActionResult GetProductsFront()
        {
            IList<Product> products = new List<Product>
            {
                new Product {Id = 1, Name = "MacBook Air", Price = 4500, Category = "Laptops"},
                new Product {Id = 2, Name = "iPhone 12", Price = 3200, Category = "Mobiles"},
                new Product {Id = 3, Name = "Wireless Mouse", Price = 120, Category = "Accessories"}
            };
            return View("index", products);
        }
    }
}
