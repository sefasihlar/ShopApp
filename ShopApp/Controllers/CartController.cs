using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Concrete;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Entites;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private UserManager<AppUser> _userManager;
        CartManager _cartManager = new CartManager(new EfCoreCartDal());

        public CartController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var cart = _cartManager.GetCartByUserId(_userManager.GetUserId(User));
            return View(new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(x=> new CartItemModel()
                {
                    CartItemId = x.Id,
                    ProductId = x.ProductId,
                    Name = x.Product.Name,
                    Price =Convert.ToDecimal(x.Product.Price),
                    ImageUrl = x.Product.ImageUrl,
                    Quantity = x.Quantity


                     
                }).ToList()
            });
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var userId = _userManager.GetUserId(User);
            _cartManager.AddToCart(userId,productId,quantity);
            return RedirectToAction("Index","Cart");
        }

        [HttpPost]
        public IActionResult DeleteFromCart(int productId)
        {
            _cartManager.DeleteFromCart(_userManager.GetUserId(User),productId);
            return RedirectToAction("Index","Cart");
        }
    }
}
