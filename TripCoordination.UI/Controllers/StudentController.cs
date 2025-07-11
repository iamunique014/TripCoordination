using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;

namespace TripCoordination.Controllers
{
    public class StudentController : Controller
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


        public StudentController(/*Logger<AdminController> logger*/ ITownRepository townRepository, IResidenceRepository residenceRepository, IUserRepository userRepository, IProfileRepository profileRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, ITripRepository tripRepository, ITripParticipantRepository tripParticipantRepository, ITripDestinationTownRepository tripDestinationTownRepository)
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MyTrips()
        {
            ViewData["ShowSideBar"] = true;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var myTrip = await _userRepository.GetUserJoinedTrips(userId);
            //TempData["Success"] = "You’ve successfully joined the trip.";
            return View(myTrip);
        }

        //[HttpPost]
        //public async Task<IActionResult> LeaveTrip(int tripParticipantId)
        //{
        //    await _tripRepository.RemoveTripParticipantAsync(tripParticipantId);
        //    TempData["Success"] = "You’ve successfully left the trip.";
        //    return RedirectToAction("MyTrips");
        //}
    }
}
