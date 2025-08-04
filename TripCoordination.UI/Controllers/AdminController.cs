using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;

namespace TripCoordination.Controllers
{
    public class AdminController : Controller
    {
        //private readonly ILogger<AdminController> _logger;

        private readonly ITownRepository _townRepository;        
        //private readonly IUserRepository _userRepository;
        //private readonly IRoleRepository _roleRepository;
        private readonly ITripRepository _tripRepository;
        //private readonly IProfileRepository _profileRepository;
        //private readonly IUserRoleRepository _UserRoleRepository;
        private readonly IResidenceRepository _residenceRepository;
        //private readonly ITripParticipantRepository _tripParticipantRepository;
        //private readonly ITripDestinationTownRepository _tripDestinationTownRepository;
        private readonly IAdminDashboardRepository _adminDashboardRepository;
        

        public AdminController(ITownRepository townRepository, IResidenceRepository residenceRepository, ITripRepository tripRepository, IAdminDashboardRepository adminDashboardRepository)
        {
            //_logger = logger;
            _townRepository = townRepository;
            _residenceRepository = residenceRepository;
            //_userRepository = userRepository;
           // _profileRepository = profileRepository;
            //_UserRoleRepository = userRoleRepository;
            //_roleRepository = roleRepository;
            _tripRepository = tripRepository;
           //_tripParticipantRepository = tripParticipantRepository;
            //_tripDestinationTownRepository = tripDestinationTownRepository;
            _adminDashboardRepository = adminDashboardRepository;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDashboard()
        {
            try
            {
                ViewData["ShowSidebar"] = true;

                var viewModel = new AdminDashboardViewModel
                {
                    UserStats = await _adminDashboardRepository.GetUserStats(),
                    TripStats = await _adminDashboardRepository.GetTripStats(),
                    RecentActivities = await _adminDashboardRepository.GetRecentActivityViewModels()
                };

                return View(viewModel);
            }
            catch(Exception)
            {
                // must Log the exception before returning an error view (not yet implemented)
                TempData["Info"] = "An error occurred while loading the dashboard.";
                var model = new AdminDashboardViewModel();
                return View(model);
            }         
        }

        //---------------RESIDENCE MANAGEMENT-------------------//

        public async Task<IActionResult> ManageResidences()
        {
            ViewData["ShowSidebar"] = true;
            var residences = await _residenceRepository.GetAllAsync();
            return View(residences);
        }

        public async Task<IActionResult> CreateResidence()
        {
            ViewData["ShowSidebar"] = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateResidence(Residence residence)
        {
            ViewData["ShowSidebar"] = true;
            try
            {
                if (!ModelState.IsValid)
                    return View(residence);

                bool addResidence = await _residenceRepository.AddAsync(residence);
                if (addResidence)
                {
                    TempData["Success"] = "New Residence created successfully!";
                    return RedirectToAction(nameof(ManageResidences));
                }
                else
                {
                    TempData["Error"] = "Failed to created new residence";
                    return View(residence);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong, Please try again later!";
            }
            return RedirectToAction(nameof(ManageResidences));
        }

        public async Task<IActionResult> EditResidence(int residenceID)
        {
            //Need to get the exact town to edit
            ViewData["ShowSidebar"] = true;
            var residence = await _residenceRepository.GetByIdAsync(residenceID);
            return View(residence);
        }

        [HttpPost]
        public async Task<IActionResult> EditResidence(Residence residence)
        {
            ViewData["ShowSidebar"] = true;
            try
            {
                if (!ModelState.IsValid)
                    return View(residence);

                bool updateRecord = await _residenceRepository.UpdateAsync(residence);

                if (updateRecord)
                {
                    TempData["Success"] = "Residence updated successfully!";
                    return RedirectToAction(nameof(ManageResidences));
                }
                else
                {
                    TempData["Error"] = "Failed to update residence";
                    return View(residence);
                }
                    
            }

            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong, Please try again later!";
            }
            return RedirectToAction(nameof(ManageResidences));
        }


        public async Task<IActionResult> DeleteResidence(int residenceID)
        {
            var deleteResult = await _residenceRepository.DeleteAsync(residenceID);
            if (deleteResult)
            {
                TempData["Success"] = "Residence Deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Failed to Delete residence";
            }
            return RedirectToAction(nameof(ManageResidences));
        }

        //=================================================//
        //---------------TOWN MANAGEMENT-------------------//
        //=================================================//

        public async Task<IActionResult> ManageTowns()
        {
            ViewData["ShowSidebar"] = true;
            var towns = await _townRepository.GetAllAsync();
            return View(towns);

        }

        public async Task<IActionResult> CreateTown()
        {
            ViewData["ShowSidebar"] = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTown(Town town)
        {
            ViewData["ShowSidebar"] = true;
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Failed to add new town, Please check all fields!";
                    return View(town);
                }
                   

                bool addTown = await _townRepository.AddAsync(town);
                if (addTown)
                {
                    TempData["Success"] = "New town created successfully";
                    return RedirectToAction(nameof(ManageTowns));
                }
                else
                {
                    TempData["Error"] = "Failed to add new town";
                    return View(town);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong Please try again later!!!";
                return View(town);
            }
        }

        public async Task<IActionResult> EditTown(int townID)
        {
            //Need to get the exact town to edit
            ViewData["ShowSidebar"] = true;
            var town = await _townRepository.GetByIdAsync(townID);
            return View(town);
        }

        [HttpPost]
        public async Task<IActionResult> EditTown(Town town)
        {
            ViewData["ShowSidebar"] = true;
            try
            {
                if (!ModelState.IsValid)
                    return View(town);

                bool updateRecord = await _townRepository.UpdateAsync(town);

                if (updateRecord)
                {
                    TempData["Success"] = "Town updated successfully";
                    return RedirectToAction(nameof(ManageTowns));
                }
                else
                {
                    TempData["Error"] = "Failed to update record";
                    return View(town);
                }
            }

            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong, Please try again later!";
            }
            return RedirectToAction(nameof(ManageTowns));
        }

        public async Task<IActionResult> DeleteTown(int townID)
        {
            var deleteResult = await _townRepository.DeleteAsync(townID);
            if (deleteResult)
            {
                TempData["Success"] = "Town Deleted successfully";
            }
            else
            {
                TempData["Error"] = "Something went wrong, Please try again later!";
            }
            return RedirectToAction(nameof(ManageTowns));
        }

        [HttpGet]
        public async Task<IActionResult> ManageTrips()
        {
            ViewData["ShowSidebar"] = true;

            var trip = await _tripRepository.GetAllAsync();

            return View(trip);
        }

        public async Task<IActionResult> TripWithDestinations(int tripID)
        {
            ViewData["ShowSidebar"] = true;
            var trip = await _tripRepository.GetTripWithDestinations(tripID);
            return View(trip);  // this returns a single TripWithDestinationsViewModel
        }

        public async Task<IActionResult> CreateTrip()
        {
            ViewData["ShowSidebar"] = true;
            return View();
        }
    }
}
