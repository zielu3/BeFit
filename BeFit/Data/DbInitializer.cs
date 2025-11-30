using BeFit.Models;
using Microsoft.AspNetCore.Identity;

namespace BeFit.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // konto administratora
            var adminEmail = "admin@befit.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            
            if (adminUser == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "Admin123!");
                
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            // konto testowe u¿ytkownika
            var testUserEmail = "igor@wp.pl";
            var testUser = await userManager.FindByEmailAsync(testUserEmail);
            
            if (testUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = testUserEmail,
                    Email = testUserEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Igor123!");
                
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}
