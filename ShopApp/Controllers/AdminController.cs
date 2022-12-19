using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Concrete;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Entites;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    public class AdminController : Controller
    {
        ProductManager ip = new ProductManager(new EfCoreProductDal());
        public IActionResult Index()
        {
            return View(new ProductModel()
            {
                Products = ip.GetALl()
            });
        }
        [HttpGet]
        public IActionResult CreateProdcut()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(AddProductModel model)
        {
            var values = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                Gender = model.Gender,
                ImageUrl = model.ImageUrl,
            };

            ip.Create(values);

            return Redirect("Index");    
        }
    }
}
