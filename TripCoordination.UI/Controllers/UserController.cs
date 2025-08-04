using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;

namespace TripCoordination.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IUserRepository userRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUserRole(string UserID)
        {
            ViewData["ShowSidebar"] = true;
            
            var user = await _userRepository.GetUserWithRole(UserID);
            if (user == null)
            {
                TempData["Error"] = "User not found!";
                return RedirectToAction("ViewUsers");
            }
            // This pre-selects the current role in the dropdown
            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            ViewBag.AvailableRoles = new SelectList(allRoles, user.RoleName); // user.RoleName is pre-selected
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUserRole(UserWithRoleViewModel model)
        {
            ViewData["ShowSidebar"] = true;
            if (model.Id == _userManager.GetUserId(User))
            {
                TempData["Failure"] = "Failure, You cannot change your role!";
                return RedirectToAction("ViewUsers");
            }
            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, roles);
                    await _userManager.AddToRoleAsync(user, model.RoleID);

                    TempData["Success"] = "User role updated successfully.";
                }
                else
                {
                    TempData["Error"] = "User not found!";
                }
            }
            catch (Exception ex)
            {
                TempData["Failure"] = $"Something went wrong: {ex.Message}";
            }

            return RedirectToAction("ViewUsers");
        }
    }
}
