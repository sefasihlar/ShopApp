using Microsoft.AspNetCore.Mvc;
using ShopApp.Entites;

namespace ShopApp.WebUI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateProdcut()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product entity)
        {
            return Redirect("Index");    
        }
    }
}
