using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TripCoordination.Data.Repository;

namespace TripCoordination.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ViewUsers()
        {
            ViewData["ShowSidebar"] = true;
            var users = await _userRepository.GetAllAsync();
            return View(users);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BlockUser(string UserID)
        {
            if (UserID == /*User.FindFirstValue(ClaimTypes.NameIdentifier)*/ /*User.Identity.Name*/ _userManager.GetUserId(User))
            {
                TempData["Failure"] = "Failure, You cannot block yourself!";
                return RedirectToAction("ViewUsers");
            }
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
            catch (Exception)
            {
                TempData["Failure"] = "An Error While blocking the user";
            }

            return RedirectToAction("ViewUsers");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnblockUser(string UserID)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(UserID);
                if(user != null)
                {
                    await _userManager.SetLockoutEndDateAsync(user, null);
                    TempData["Success"] = "User unblocked successfully.";
                }
                else
                {
                    TempData["Error"] = "User not found!.";
                }
            }
            catch(Exception)
            {
                TempData["Failure"] = "An Error occured while unblocking the user!";
            }
            return RedirectToAction("ViewUsers");
        }
    }
}
