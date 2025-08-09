using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Helpers.Extensions;

namespace TripCoordination.Controllers
{
    public class DemoLoginController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DemoLoginController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAs(string role)
        {
            string email = role.ToLower() switch
            {
                "admin" => "admin@demo.com",
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
                        return RedirectToAction("OrganizerDashboard", "TripCreator");

                    if (await _userManager.IsInRoleAsync(user, "Student"))
                        return RedirectToAction("StudentDashboard", "Student");

                    // Default fallback
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["Error"] = "Demo login failed. Please check credentials or roles.";
            return RedirectToAction("Index", "Home");
        }
    }
}
