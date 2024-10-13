using DataAccess.Models;
using Microsoft.AspNetCore.Identity;

namespace homework_8.Helper
{
    public enum Roles
    {
        User,
        Admin
    }
    public static class Seeder
    {
        public static async Task SeedRoles(this IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (string role in Enum.GetNames(typeof(Roles)))
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        public static async Task SeedAdmin(this IServiceProvider serviceProvider)
        {
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            string username = "Admin@gmail.com";
            string pass = "Qwerty123$";
            User user = new User() { UserName=username,Email=username,EmailConfirmed=true};
            var res = userManager.CreateAsync(user, pass).Result;
            if (res.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}
