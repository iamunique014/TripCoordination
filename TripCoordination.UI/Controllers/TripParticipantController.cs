using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripCoordination.Data.Repository;
using TripCoordination.ViewModel;

namespace TripCoordination.Controllers
{
    public class TripParticipantController : Controller
    {
        private readonly ITripParticipantRepository _tripParticipantRepository;

        public TripParticipantController(ITripParticipantRepository tripParticipantRepository)
        {
            _tripParticipantRepository = tripParticipantRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ViewParticipants(int tripID)
        {
            ViewData["ShowSidebar"] = true;

            var tripParticipants = await _tripParticipantRepository.GetParticipantsByTripIDAsync(tripID);

            return View(tripParticipants);
        }

        [Authorize(Roles = "Admin, Organizer")]
        public async Task<IActionResult> DeleteTripParticipant(int tripParticipantID, int tripID)
        {
            ViewData["ShowSidebar"] = true;

            var result = await _tripParticipantRepository.DeleteTripParticipantAsync(tripParticipantID, tripID);

            if (!result)
            {
                TempData["Error"] = "Failed to remove participant!";
            }
            else
            {
                TempData["Succes"] = "Participant Deleted Succesfuly";
            }

            return RedirectToAction("ViewParticipants", "TripParticipant", new { tripID });
        }

        [HttpPost]
        public async Task<IActionResult> LeaveTrip(int tripParticipantID)
        {
            try
            {
                bool success = await _tripParticipantRepository.LeaveTripAsync(tripParticipantID);

                if (success)
                {
                    TempData["Success"] = "You have successfully left the trip.";
                }
                else
                {
                    TempData["Error"] = "Failed to leave the trip.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while trying to leave the trip.";
                Console.WriteLine(ex);
            }

            return RedirectToAction("MyTrips", "Student");
        }
    }
}
