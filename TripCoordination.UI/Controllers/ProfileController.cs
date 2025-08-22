using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Security.Policy;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;
using TripCoordination.ViewModel;

namespace TripCoordination.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IResidenceRepository _residenceRepository;
        public ProfileController(IProfileRepository profileRepository, IResidenceRepository residenceRepository)
        {
            _profileRepository = profileRepository;
            _residenceRepository = residenceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {
            ViewData["ShowSidebar"] = true;
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = await _profileRepository.GetUserProfileAsync(userID);

            if (profile == null)
            {
                return RedirectToAction("CompleteProfile");
            }

            var residences = await _residenceRepository.GetAllAsync();

            var model = new CreateProfileViewModel
            {
                Title = profile.Title,
                Name = profile.Name,
                Surname = profile.Surname,
                Email = profile.Email,
                PhoneNumber = profile.PhoneNumber,
                Address = profile.Address,
                DateOfBirth = profile.DateOfBirth,
                ResidenceID = profile.ResidenceID,
                UserID = profile.UserID,
                AvailableResidences = residences.Select(r => new SelectListItem
                {
                    Value = r.ResidenceID.ToString(),
                    Text = r.Name
                }).ToList()
            };

            return View(model);
        } 


        public async Task<IActionResult> CompleteProfile(string? returnUrl = null)
        {
            ViewData["ShowSidebar"] = true;
            ViewData["ReturnUrl"] = returnUrl;

            var residence = await _residenceRepository.GetAllAsync();

            var model = new CreateProfileViewModel
            {
                AvailableResidences = residence.Select(r => new SelectListItem
                {
                    Value = r.ResidenceID.ToString(),
                    Text = r.Name
                }).ToList(),

                DateOfBirth = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteProfile(CreateProfileViewModel profile, string? returnUrl = null)
        {
            ViewData["ShowSidebar"] = true;

            if (!ModelState.IsValid)
            {
                var residences = await _residenceRepository.GetAllAsync();
                profile.AvailableResidences = residences.Select(r => new SelectListItem
                {
                    Value = r.ResidenceID.ToString(),
                    Text = r.Name
                }).ToList();

                ViewData["ReturnUrl"] = returnUrl;
                return View(profile); // Return the same model with validation errors
            }

            try
            {
                profile.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                bool addProfile = await _profileRepository.AddAsync(profile);

                if (addProfile)
                {
                    // Security check: only allow local redirects
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    TempData["Success"] = "Profile created successfully";
                    return RedirectToAction(nameof(ViewProfile));
                }

                TempData["Error"] = "Failed to create profile";
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Something went wrong. Please try again later.";
                Console.WriteLine(ex);
            }

            // Rebuild the dropdown and return view
            var residenceList = await _residenceRepository.GetAllAsync();
            profile.AvailableResidences = residenceList.Select(r => new SelectListItem
            {
                Value = r.ResidenceID.ToString(),
                Text = r.Name
            }).ToList();

            return View(profile);
        }

        public async Task<IActionResult> EditProfile()
        {
            ViewData["ShowSidebar"] = true;
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = await _profileRepository.GetUserProfileAsync(userID);
            var residences = await _residenceRepository.GetAllAsync();

            var model = new CreateProfileViewModel
            {
                Title = profile.Title,
                Name = profile.Name,
                Surname = profile.Surname,
                Email = profile.Email,
                PhoneNumber = profile.PhoneNumber,
                Address = profile?.Address,
                DateOfBirth = profile?.DateOfBirth ?? DateTime.Now,
                ResidenceID = profile?.ResidenceID ?? 0,
                UserID = userID,
                AvailableResidences = residences.Select(r => new SelectListItem
                {
                    Value = r.ResidenceID.ToString(),
                    Text = r.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(CreateProfileViewModel model)
        {
            ViewData["ShowSidebar"] = true;

            if (!ModelState.IsValid)
            {
                var residences = await _residenceRepository.GetAllAsync();
                model.AvailableResidences = residences.Select(r => new SelectListItem
                {
                    Value = r.ResidenceID.ToString(),
                    Text = r.Name
                }).ToList();

                return View(model);
            }

            try
            {
                model.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var profile = new Profile
                {
                    UserID = model.UserID,
                    Title = model.Title,
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    DateOfBirth = model.DateOfBirth,
                    ResidenceID = model.ResidenceID
                };

                var success = await _profileRepository.UpdateAsync(profile);

                if (success)
                {
                    TempData["Success"] = "Profile updated successfully";
                    return RedirectToAction("ViewProfile");
                }

                TempData["Error"] = "Failed to update profile";
            }
            catch (Exception ex)
            {
                TempData["message"] = "Something went wrong. Please try again!";
                Console.WriteLine(ex);
            }

            var residencesList = await _residenceRepository.GetAllAsync();
            model.AvailableResidences = residencesList.Select(r => new SelectListItem
            {
                Value = r.ResidenceID.ToString(),
                Text = r.Name
            }).ToList();

            return View(model);
        }
    }
}
