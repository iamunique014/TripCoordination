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

        public async Task<IActionResult> CompleteProfile()
        {
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = await _profileRepository.GetByIdAsync(UserID);
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteProfile(Profile profile)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(profile);

                profile.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                bool addProfile = await _profileRepository.AddAsync(profile);
                if (addProfile)
                {
                    TempData["msg"] = "Sucessfully Added";
                }
                else
                {
                    TempData["msg"] = "Could not add";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Hebana!! Something went wrong!!!";
            }
            return RedirectToAction(nameof(CompleteProfile));
        }


    }
}
