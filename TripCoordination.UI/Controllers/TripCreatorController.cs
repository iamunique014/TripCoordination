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
        public async Task<IActionResult> MyTrips()
        {
            ViewData["ShowSidebar"] = true;

            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var trip = await _tripRepository.GetAllUserTripsAsync(userID);

            return View(trip);
        }
    }
}
