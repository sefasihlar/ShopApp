using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopApp.Business.Concrete;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Entites;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    public class ShopController : Controller
    {
        ProductManager ip = new ProductManager(new EfCoreProductDal());
        CategoryManager ic = new CategoryManager(new EfCoreCategoryDal());
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllList()
        {
            var values = ip.GetALl();
            return View(values);
        }

        public IActionResult ManList()
        {
            var values = ip.GetALl().Where(x => x.Gender == "E").ToList();
            return View(values);
        }

        public IActionResult WomenList()
        {
            var values = ip.GetALl().Where(x => x.Gender == "K").ToList();
            return View(values);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = ip.GetById((int)id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        public PartialViewResult CategoryList()
        {

            
            return PartialView(new CategoryListViewModel()
            {
                kategoriler = ic.GetALl()
            });
            
        }

    }
}
