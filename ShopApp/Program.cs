using Autofac.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Entites;
using ShopApp.WebUI.MailServices;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Security.Principal;
using SendGrid;
using System.Reflection;
using ShopApp.WebUI.Identity;

var builder = WebApplication.CreateBuilder(args);

RoleManager<IdentityRole> roleManager;
RoleManager<AppUser> userManager;


//start Authorization
builder.Services.ConfigureApplicationCookie(o =>
{
    o.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    o.SlidingExpiration = true;
});

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddAuthentication(
   )
    .AddCookie(option =>
    {
        option.LoginPath = "Account/Login";

        option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        option.Cookie = new CookieBuilder
        {
            HttpOnly = true,
            Name = "Sefa.Buraya.Saldirmasin",
            SameSite = SameSiteMode.Strict
        };

    });


builder.Services.Configure<IdentityOptions>(options =>
{


    options.Lockout.AllowedForNewUsers = true;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;



});

Assembly.GetExecutingAssembly();


builder.Services.AddDbContext<ShopContext>();
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<ShopContext>()
    .AddDefaultTokenProviders();
//Finish Authorization 

// Add services to the container.

builder.Services.AddDataProtection()
    .DisableAutomaticKeyGeneration();

builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

    options.Cookie = new CookieBuilder()
    {
        HttpOnly = true,
        Name = "ShopApp.Security.Cookie"
    };
});


//asp.net core6 da routing işlemleri hatasız yapmak için bu işlem gerekli
builder.Services.AddMvc();
builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
});
//işlemler bitti



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    SeedDatabase.seed();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

//categorilerileri göre filtreleme işlemi yapılması için bu işlemler yeniden tanimlanmalı
app.UseMvc(Route =>
{
    Route.MapRoute(
       name: "adminProducts",
       template: "admin/products",
       defaults: new { controller = "Admin", action = "Index" }
       );

    Route.MapRoute(
       name: "adminProductsEdit",
       template: "admin/products/{id?}",
       defaults: new { controller = "Admin", action = "EditProduct" }
       );

    Route.MapRoute(
       name: "AllList",
       template: "AllList/{category?}",
       defaults: new { controller = "Shop", action = "AllList" }
       );

    Route.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}"
        );

    Route.MapRoute(
        name: "cart",
        template: "cart",
        defaults: new { controller = "Cart", action = "Index" }
        );

    Route.MapRoute(
        name: "checkout",
        template: "checkout",
        defaults: new { controller = "Cart", action = "Checkout" }
        );

    Route.MapRoute(
       name: "orders",
       template: "orders",
       defaults: new { controller = "Cart", action = "GetOrders" }
       );


});
//işlemler tanimlandi ve bitti



app.Run();
