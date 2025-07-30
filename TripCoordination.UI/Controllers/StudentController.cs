using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.Repository;

namespace TripCoordination.Controllers
{
    public class StudentController : Controller
    {
        //private readonly ILogger<AdminController> _logger;

        //private readonly ITownRepository _townRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStudentDashboardRepository _studentDashboardRepository;
        //private readonly IRoleRepository _roleRepository;
        //private readonly ITripRepository _tripRepository;
        //private readonly IProfileRepository _profileRepository;
        //private readonly IUserRoleRepository _UserRoleRepository;
        //private readonly IResidenceRepository _residenceRepository;
        //private readonly ITripParticipantRepository _tripParticipantRepository;
        //private readonly ITripDestinationTownRepository _tripDestinationTownRepository;


        //public StudentController(/*Logger<AdminController> logger*/IStudentDashboardRepository studentDashboardRepository ,ITownRepository townRepository, IResidenceRepository residenceRepository, IUserRepository userRepository, IProfileRepository profileRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, ITripRepository tripRepository, ITripParticipantRepository tripParticipantRepository, ITripDestinationTownRepository tripDestinationTownRepository)
        //{
        //    //_logger = logger;
        //    _townRepository = townRepository;
        //    _residenceRepository = residenceRepository;
        //    _userRepository = userRepository;
        //    _studentDashboardRepository = studentDashboardRepository;
        //    _profileRepository = profileRepository;
        //    _UserRoleRepository = userRoleRepository;
        //    _roleRepository = roleRepository;
        //    _tripRepository = tripRepository;
        //    _tripParticipantRepository = tripParticipantRepository;
        //    _tripDestinationTownRepository = tripDestinationTownRepository;
        //}

        public StudentController(IStudentDashboardRepository studentDashboardRepository /*IUserRepository userRepository*/)
        {
           // _userRepository = userRepository;
            _studentDashboardRepository = studentDashboardRepository;
        }
            
        [Authorize]
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
            return View(myTrip);
        }
        [HttpGet]
        [Authorize(Roles = "Student, User")]
        public async Task<IActionResult> StudentDashboard()
        {
            ViewData["ShowSideBar"] = true;

            string userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var model = new StudentDashboardViewModel
            {
                UpcomingTrip = await _studentDashboardRepository.GetNextUpcomingTrip(userID),
                RecentTripRequests = await _studentDashboardRepository.GetRecentTripRequests(userID)
            };

            return View(model);
        }
    }
}
