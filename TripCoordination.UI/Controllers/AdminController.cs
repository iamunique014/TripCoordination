using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;
using TripCoordination.ViewModel;

namespace TripCoordination.Controllers
{
    public class AdminController : Controller
    {
        //private readonly ILogger<AdminController> _logger;

        private readonly ITownRepository _townRepository;        
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRoleRepository _UserRoleRepository;
        private readonly IResidenceRepository _residenceRepository;
        private readonly ITripParticipantRepository _tripParticipantRepository;
        private readonly ITripDestinationTownRepository _tripDestinationTownRepository;
        

        public AdminController(/*Logger<AdminController> logger*/ ITownRepository townRepository, IResidenceRepository residenceRepository, IUserRepository userRepository, IProfileRepository profileRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, ITripRepository tripRepository, ITripParticipantRepository tripParticipantRepository, ITripDestinationTownRepository tripDestinationTownRepository)
        {
            //_logger = logger;
            _townRepository = townRepository;
            _residenceRepository = residenceRepository;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _UserRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _tripRepository = tripRepository;
            _tripParticipantRepository = tripParticipantRepository;
            _tripDestinationTownRepository = tripDestinationTownRepository;
        }

        public IActionResult AdminDashboard()
        {
            ViewData["ShowSidebar"] = true;
            return View();
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
                    TempData["msg"] = "Successfully Added";
                else
                    TempData["msg"] = "Oh Hell Nah";
            }

            catch (Exception ex)
            {
                TempData["msg"] = "Seriously!!!!!";
            }
            return RedirectToAction(nameof(ManageResidences));
        }


        public async Task<IActionResult> DeleteResidence(int residenceID)
        {
            var deleteResult = await _residenceRepository.DeleteAsync(residenceID);
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
