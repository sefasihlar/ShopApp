using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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

            return RedirectToAction("Index");    
        }
        [HttpPost]
        public IActionResult ProductDelete(int id)
        {
            var values = ip.GetById(id);
            ip.Delete(values);
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var values = ip.GetById((int)id);

            if (values == null)
            {
                return NotFound();
            }
    
            var model = new AddProductModel()
            {
                Id = values.Id,
                    Name = values.Name,
                        Price = values.Price,
                            ImageUrl = values.ImageUrl,
                                Gender = values.Gender,
                                    Condition = values.Condition,
                
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(AddProductModel model)
        {
            var entity = ip.GetById(model.Id);

            if (entity == null)
            {
                return NotFound();
            }

            entity.Name= model.Name;
            entity.Price= model.Price;
            entity.Gender = model.Gender;
            entity.ImageUrl = model.ImageUrl;
            entity.Condition = model.Condition;
            ip.Update(entity);
            return RedirectToAction("Index");
        }
    }
}
