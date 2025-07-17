using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TripCoordination.Helpers.Extensions;

namespace TripCoordination.Controllers
{
    public class DemoLoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public DemoLoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAs(string role)
        {
            string email = role.ToLower() switch
            {
                "admin" => "mradmin@gmail.com",
                "student" => "student@demo.com",
                "organizer" => "organizer@demo.com",
                _ => null
            };

            if (email == null)
            {
                TempData["Error"] = "Invalid role.";
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await _signInManager.SignOutAsync(); // Clear existing session

                var result = await _signInManager.PasswordSignInAsync(user, "Demo@123", isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    TempData["Success"] = $"You are logged in as Demo {role.Capitalize()}";

                    // Redirect based on role
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                        return RedirectToAction("AdminDashboard", "Admin");

                    if (await _userManager.IsInRoleAsync(user, "Organizer"))
                        return RedirectToAction("MyTrips", "TripCreator");

                    if (await _userManager.IsInRoleAsync(user, "Student"))
                        return RedirectToAction("Index", "Home");

                    // Default fallback
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["Error"] = "Demo login failed. Please check credentials or roles.";
            return RedirectToAction("Index", "Home");
        }
    }
}
