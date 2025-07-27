using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TripCoordination.Data.Repository;

namespace TripCoordination.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IUserRepository userRepository, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> ViewUsers()
        {
            ViewData["ShowSidebar"] = true;
            var users = await _userRepository.GetAllAsync();
            return View(users);
        }

        public async Task<IActionResult> BlockUser(string UserID)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(UserID);
                if (user != null)
                {
                    await _userManager.SetLockoutEnabledAsync(user, true);
                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
                    TempData["Success"] = "User blocked successfully.";
                }
                else
                {
                    TempData["Error"] = "User Not Found!";
                }
            }
            catch(Exception ex)
            {
                TempData["Failure"] = "An Error While blocking the user";
            }
            
            return RedirectToAction("ViewUsers");
        }
    }
}
