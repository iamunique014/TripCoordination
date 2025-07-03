using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;

namespace TripCoordination.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {
            ViewData["ShowSidebar"] = true;
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = await _profileRepository.GetUserProfileAsync(UserID);
            if (profile != null)
            {
                return View(profile);
            }
            else
            {
                return RedirectToAction("CompleteProfile");
            }
        } 


        public async Task<IActionResult> CompleteProfile()
        {
            ViewData["ShowSidebar"] = true;
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = await _profileRepository.GetUserProfileAsync(userID);

            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteProfile(Profile profile)
        {
            ViewData["ShowSidebar"] = true;
            try
            {
                if (!ModelState.IsValid)
                    return View(profile);

                profile.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                bool addProfile = await _profileRepository.AddAsync(profile);
                if (addProfile)
                {
                    TempData["Success"] = "Profile created successfully";
                    return RedirectToAction(nameof(ViewProfile));
                }
                else
                {
                    TempData["Error"] = "Failed to create profile";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Hebana!! Something went wrong!!!";
            }
            return RedirectToAction(nameof(CompleteProfile));
        }

        public async Task<IActionResult> EditProfile()
        {
            ViewData["ShowSidebar"] = true;
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = await _profileRepository.GetUserProfileAsync(UserID);
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(Profile profile)
        {
            ViewData["ShowSidebar"] = true;
            try
            {
                if (!ModelState.IsValid)
                    return View(profile);

                profile.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                bool updateRecord = await _profileRepository.UpdateAsync(profile);

                if (updateRecord)
                {
                    TempData["Success"] = "Profile update Successfully";
                    return RedirectToAction("ViewProfile");
                }
                else
                {
                    TempData["Error"] = "Failed to update profile";
                }
                    
            }

            catch (Exception ex)
            {
                TempData["message"] = "Something went wrong please try again !!";
                Console.WriteLine(ex.ToString());
            }
            return View(profile);
        }
    }
}
