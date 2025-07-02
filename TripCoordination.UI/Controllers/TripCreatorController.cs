using Microsoft.AspNetCore.Mvc;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;
using TripCoordination.UI.Controllers;

namespace TripCoordination.Controllers
{
    public class TripCreatorController : Controller
    {
        private readonly ILogger<TripCreatorController> _logger;

        private readonly IResidenceRepository _residenceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRoleRepository _UserRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITripRepository _tripRepository;

        public TripCreatorController(ILogger<TripCreatorController> logger, ITripRepository tripRepository, IResidenceRepository residenceRepository, IUserRepository userRepository, IProfileRepository profileRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _logger = logger;
            _tripRepository = tripRepository;
            _residenceRepository = residenceRepository;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _UserRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateTrip()
        {
            return View();
        }

        public IActionResult OrganizerDashboard()
        {
            ViewData["ShowSidebar"] = true;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MyTrips()
        {
            ViewData["ShowSidebar"] = true;

            var trip = await _tripRepository.GetAllAsync();

            return View(trip);
        }
    }
}
