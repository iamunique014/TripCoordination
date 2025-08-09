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

        public StudentController(IStudentDashboardRepository studentDashboardRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
            try
            {
                ViewData["ShowSideBar"] = true;

                string userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

                if (string.IsNullOrEmpty(userID))
                {
                    TempData["ErrorMessage"] = "You are not authorized to access this page. Please log in to continue.";
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }
                else
                {
                    var model = new StudentDashboardViewModel
                    {
                        UpcomingTrip = await _studentDashboardRepository.GetNextUpcomingTrip(userID),
                        RecentTripRequests = await _studentDashboardRepository.GetRecentTripRequests(userID),
                        TripStats = await _studentDashboardRepository.GetStudentTripStats(userID)
                    };

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                // must Log the exception before returning an error view (not yet implemented)
                return View("Error", new { message = "An error occurred while loading the dashboard." });
            }
           
   
        }
        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetTripsJoinedChartData()
        {
            string userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var data = await _studentDashboardRepository.GetStudentMonthlyTripsJoinedCount(userID);

            return Json(data);
        }

        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetTripRequestStatusDistributionChartData()
        {
            string userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var data = await _studentDashboardRepository.GetStudentTripRequestStatusDistribution(userID);

            return Json(data);
        }
    }
}
