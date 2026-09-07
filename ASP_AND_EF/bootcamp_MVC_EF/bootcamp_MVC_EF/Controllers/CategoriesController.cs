using bootcamp_MVC_EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_MVC_EF.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetCategories()
        {
            IList<Category> categories = new List<Category>
            {
                new Category { Id = 1, Name = "Laptops" },
                new Category { Id = 2, Name = "Mobiles" },
                new Category { Id = 3, Name = "Accessories" }
            };

            return Ok(categories);
        }
        public ActionResult GetCategoriesFront()
        {
            IList<Category> categories = new List<Category>
            {
                new Category { Id = 1, Name = "Laptops" },
                new Category { Id = 2, Name = "Mobiles" },
                new Category { Id = 3, Name = "Accessories" }
            };

            return View("index", categories);
        }
    }
}
