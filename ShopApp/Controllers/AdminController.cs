using Microsoft.AspNetCore.Authorization;
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
        CategoryManager _category = new CategoryManager(new EfCoreCategoryDal());
        OrderManager _orderManager = new OrderManager(new EfCoreOrderDal());

        public IActionResult AdminHomePage()
        {
            return View(new ProductModel()
            {
                Products = ip.GetALl()
            });
        }


        public IActionResult OrderList()
        {
            var values = _orderManager.GetAllOrders();
            return View(values);
        }

        public IActionResult OrderDetails(int id)
        {
            var values = _orderManager.GetWithOrderId(id);
            return View(values);
        }

        public IActionResult WomenList()
        {
            return View(new ProductModel()
            {
                Products = ip.GetALl().Where(x => x.Gender == "Female").ToList(),
            });
        }

        public IActionResult ManList()
        {
            return View(new ProductModel()
            {
                Products = ip.GetALl().Where(x => x.Gender == "Female").ToList(),
            });
        }


        public IActionResult Index()
        {
            return View(new ProductModel()
            {
                Products = ip.GetALl()
            });
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View(new AddProductModel()
            {

            });
        }

        [HttpPost]
        public IActionResult CreateProduct(AddProductModel model)
        {
            if (ModelState.IsValid)
            {
                var values = new Product()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Gender = model.Gender,
                    ImageUrl = model.ImageUrl,
                };

                ip.Create(values);

                return RedirectToAction("Index", "Admin");
            }

            return View(model);




        }
        [HttpPost]
        public IActionResult ProductDelete(int id)
        {
            var values = ip.GetById(id);
            ip.Delete(values);
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var values = ip.GetByIdWithCategories((int)id);

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
                SelectedCategories = values.ProductCategories.Select(x => x.Category).ToList()

            };

            ViewBag.Categories = _category.GetALl();

            return View(model);


        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(AddProductModel model, int[] categoryIds, IFormFile file)
        {

            var entity = ip.GetById(model.Id);

            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.Gender = model.Gender;
            entity.Condition = model.Condition;
            if (file != null)
            {
                entity.ImageUrl = file.FileName;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Theme\\img\\product", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                };

            }


            ip.Update(entity, categoryIds);
            return RedirectToAction("Index", "Admin");



            ViewBag.Categories = _category.GetALl();

            return View(model);

        }


        public IActionResult CategoryList()
        {
            return View(new CategoryListModel()
            {
                Categories = _category.GetALl()
            });

        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            var values = new Category()
            {
                Name = model.Name
            };
            _category.Create(values);
            return RedirectToAction("CategoryList", "Admin");
        }
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var values = _category.GetByIdWithProducuts(id);
            return View(new CategoryModel()
            {
                Id = values.Id,
                Name = values.Name,
                Products = values.ProductCategories.Select(p => p.Product).ToList()
            });
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            var values = _category.GetById(model.Id);

            if (values == null)
            {
                return NotFound();
            }

            values.Name = model.Name;
            _category.Update(values);

            return RedirectToAction("CategoryList", "Admin");
        }

        public IActionResult DeleteCategory(int id)
        {

            var values = _category.GetById(id);
            if (values != null)
            {
                _category.Delete(values);
            }

            else
            {
                return NotFound();
            }


            return RedirectToAction("CategoryList", "Admin");
        }

        [HttpPost]
        public IActionResult DeleteFromCategory(int id, int categoryid)
        {
            _category.DeleteFromCategory(id, categoryid);
            return Redirect("/admin/editcategory/" + categoryid);
        }
    }
}
