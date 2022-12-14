using ShopApp.DataAccess.Concrete.EfCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

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

app.UseAuthorization();

//categorilerileri göre filtreleme işlemi yapılması için bu işlemler yeniden tanimlanmalı
app.UseMvc(Route =>
{
  

    Route.MapRoute(
       name: "AllList",
       template: "AllList/{category?}",
       defaults:new {controller = "Shop", action = "AllList"}
       );

    Route.MapRoute(
        name:"default",
        template: "{controller=Home}/{action=Index}/{id?}"
        );
});
//işlemler tanimlandi ve bitti

app.Run();
