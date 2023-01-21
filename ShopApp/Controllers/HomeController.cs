using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Concrete;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Entites;
using ShopApp.Models;
using System.Diagnostics;

namespace ShopApp.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        ProductManager ip = new ProductManager(new EfCoreProductDal());

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var values = ip.GetALl().Where(x=> x.Price >=100 & x.Condition=="True").ToList();
            return View(values);
        }

        //Galeri kısmı component kısmıyla yapılcak --start
        public IActionResult Womans()
        {
            var values = ip.GetALl().Where(x => x.Price >= 100 & x.Condition == "True").ToList();
            return View(values);
        }

        public IActionResult Mans()
        {
            var values = ip.GetALl().Where(x => x.Price >= 100 & x.Condition == "True").ToList();
            return View(values);
        }
        public IActionResult Kids()
        {
            var values = ip.GetALl().Where(x => x.Price >= 100 & x.Condition == "True").ToList();
            return View(values);
        }
        public IActionResult Accessories()
        {
            var values = ip.GetALl().Where(x => x.Price >= 100 & x.Condition == "True").ToList();
            return View(values);
        }
        public IActionResult Cosmatics()
        {
            var values = ip.GetALl().Where(x => x.Price >= 100 & x.Condition == "True").ToList();
            return View(values);
        }

        //galeri kısmı --finish

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GelAll()
        {

           var values =  ip.GetALl();
            return View(values);
         
        }

    
    }
}