using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Entites;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    FullName = model.FullName,
                };

                var result =await _userManager.CreateAsync(user,model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }


                ModelState.AddModelError("","Bilinmeyen bir hata oluştu lütfen tekar deneyiniz");
             
              
            }

            return View("bir hata");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model,string returnUrl = null)
        {
            returnUrl = returnUrl ?? "~/";
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "Böle bir hesap bulunamadı lüften hesap oluşrun");
                return View(model);
            }


            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }

            ModelState.AddModelError("", "Kullanıcı adı yada parola yanlış");
            return View(model);
        }
    }
}
