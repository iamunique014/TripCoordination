using Microsoft.AspNetCore.Mvc;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;

namespace TripCoordination.Controllers
{
    public class Admin : Controller
    {
        private readonly IResidenceRepository _residenceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRoleRepository _UserRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITripRepository _tripRepository;

        public Admin(IResidenceRepository residenceRepository, IUserRepository userRepository, IProfileRepository profileRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, ITripRepository tripRepository)
        {
            _residenceRepository = residenceRepository;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _UserRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _tripRepository = tripRepository;
        }

        public IActionResult AdminDashboard()
        {
            ViewData["ShowSidebar"] = true;
            return View();
        }

        public async Task<IActionResult> Add()
        {
            ViewData["ShowSidebar"] = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Residence residence)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(residence);
                bool addResidence = await _residenceRepository.AddAsync(residence);
                if (addResidence)
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
            return RedirectToAction(nameof(ManageResidences));
        }
        public async Task<IActionResult> ManageResidences()
        {
            ViewData["ShowSidebar"] = true;
            var residences = await _residenceRepository.GetAllAsync();
            return View(residences);
        }


        //=================================================//
        //                TRIP MANAGEMENT                  //
        //=================================================//

        public async Task<IActionResult> ManageTrips()
        {
            ViewData["ShowSidebar"] = true;
            var trips = await _tripRepository.GetAllAsync();
            return View(trips);
        }

        public async Task<IActionResult> CreateTrip()
        {
            ViewData["ShowSidebar"] = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip(Trip trip)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(trip);
                bool addTrip = await _tripRepository.AddAsync(trip);
                if (addTrip)
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
            return RedirectToAction(nameof(ManageResidences));
        }
    }
}
