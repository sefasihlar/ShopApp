using Microsoft.AspNetCore.Identity;
using ShopApp.Entites;

namespace ShopApp.WebUI.Identity
{
    public static class SeedIdentity
    {
        public static async Task Seed(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            var username = configuration["Data:AdminUser:username"];
            var email = configuration["Data:AdminUser:email"];
            var password = configuration["Data:AdminUser:password"];
            var role = configuration["Data:AdminUser:role"];

            if (await userManager.FindByNameAsync(username) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role));

                var user = new AppUser()
                {
                    UserName = username,
                    Email = email,
                    FullName = "Admin User",
                    EmailConfirmed = true,

                };

                var result = await userManager.CreateAsync(user,password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                
            }

        }
    }
}
