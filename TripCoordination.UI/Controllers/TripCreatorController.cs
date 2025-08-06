using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;
using TripCoordination.UI.Controllers;
using System.Threading.Tasks;

namespace TripCoordination.Controllers
{
    public class TripCreatorController : Controller
    {
        private readonly ILogger<TripCreatorController> _logger;

        private readonly IOrganizerDashboardRepository _organizerDashboardRepository;
        private readonly ITripRepository _tripRepository;

        public TripCreatorController(ILogger<TripCreatorController> logger, ITripRepository tripRepository, IOrganizerDashboardRepository organizerDashboardRepository)
        {
            _logger = logger;
            _organizerDashboardRepository = organizerDashboardRepository;
            _tripRepository = tripRepository;
        }
        [HttpGet]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> OrganizerDashboard()
        {
            try
            {
                ViewData["ShowSidebar"] = true;

                string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if(string.IsNullOrEmpty(userID))
                {
                    TempData["ErrorMessage"] = "You are not authorized to access this page. Please log in to continue.";
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }
                else
                {
                    var model = new OrganizerDashboardViewModel
                    {
                        UpcomingTrip = await _organizerDashboardRepository.GetUpcomingTrip(userID),
                        TripStats = await _organizerDashboardRepository.GetOrganizerTripStats(userID),
                        RecentTripRequests = await _organizerDashboardRepository.GetRecentTripRequests()
                    };

                    return View(model);
                }
                  
                
            }
            catch (Exception)
            {
                // must Log the exception before returning an error view (not yet implemented)
                TempData["Info"] = "An error occurred while loading the dashboard.";
                var model = new OrganizerDashboardViewModel();
                return View(model);
            }
           
        }

        [HttpGet]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> GetTripsCreatedChartData()
        {
            string userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            var data = await _organizerDashboardRepository.GetMonthlyTripCountByOrganizer(userID);            

            return Json(data);
        }

        [HttpGet]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> GetSeatUtilizationChartData()
        {
            try
            {
                string userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

                if (string.IsNullOrEmpty(userID))
                {
                    return Unauthorized();
                }

                var data = await _organizerDashboardRepository.GetTripSeatUtilizationChartData(userID);

                var chartData = data.Select(d => new
                {
                    label = d.TripTitle,
                    filled = d.SeatsFilled,
                    available = d.SeatsAvailable
                });

                return Json(chartData);
            }
            catch (Exception ex)
            {
                // log exception here
                return StatusCode(500, "Failed to load chart data.");
            }
        }


        [HttpGet]
        public async Task<IActionResult> MyTrips()
        {
            ViewData["ShowSidebar"] = true;

            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var trip = await _tripRepository.GetAllUserTripsAsync(userID);

            return View(trip);
        }
    }
}
