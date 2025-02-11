using Microsoft.AspNetCore.Identity;
using TripCoordination.Constants;

namespace TripCoordination.Areas.Identity.Data
{
    public static class DBseeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //seed roles
            var userManager = service.GetService<UserManager<IdentityUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Student.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Organizer.ToString()));

            //Creating Admin

            var user = new IdentityUser
            {
                UserName = "mradmin@gmail.com",
                Email = "mradmin@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var userInDb = await userManager.FindByEmailAsync(user.Email);

            if (userInDb == null)
            {
                await userManager.CreateAsync(user, "MrAdmin@123");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
        }
    }
}
