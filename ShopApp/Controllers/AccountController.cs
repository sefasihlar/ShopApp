using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using ShopApp.Entites;
using ShopApp.WebUI.Models;
using System.Security.Policy;

namespace ShopApp.WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]
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
                    var code =await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.ActionLink("ConfirmEmail", "Account", new
                    {
                        userId = user.Id,
                        Token = code

                    });
               
                    return RedirectToAction("Login", "Account");
                }


                ModelState.AddModelError("","Mail yada şifre yanlış");
                return View(model);

            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
         
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu Email adresiyle daha önce bir hesap oluşturulmamış");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Lütfen Hesabinizi Onaylayiniz");
                return View(model);
            }


            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index","Home");
            }

            ModelState.AddModelError("", "Kullanıcı adı yada parola yanlış");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }

        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if (userId == null || token == null)
            {
                TempData["Message"] = "Geçersiz token";
                return View();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user!=null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    TempData["Message"] = "Hesabiniz Onaylandi";
                    return View();
                }

            }

            TempData["Message"] = "Hesabınız Onaylanmadı";
            return View();
        }
    }
}
