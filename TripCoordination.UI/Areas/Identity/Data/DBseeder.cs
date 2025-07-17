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

            // === DEMO: Admin ===
            var admin = new IdentityUser
            {
                UserName = "admin@demo.com",
                Email = "admin@demo.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var adminInDb = await userManager.FindByEmailAsync(admin.Email);
            if (adminInDb == null)
            {
                await userManager.CreateAsync(admin, "Demo@123"); // Replace with strong demo password
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }

            // === DEMO: Student ===
            var student = new IdentityUser
            {
                UserName = "student@demo.com",
                Email = "student@demo.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var studentInDb = await userManager.FindByEmailAsync(student.Email);
            if (studentInDb == null)
            {
                await userManager.CreateAsync(student, "Demo@123");
                await userManager.AddToRoleAsync(student, Roles.Student.ToString());
            }

            // === DEMO: Organizer ===
            var organizer = new IdentityUser
            {
                UserName = "organizer@demo.com",
                Email = "organizer@demo.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var organizerInDb = await userManager.FindByEmailAsync(organizer.Email);
            if (organizerInDb == null)
            {
                await userManager.CreateAsync(organizer, "Demo@123");
                await userManager.AddToRoleAsync(organizer, Roles.Organizer.ToString());
            }


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
