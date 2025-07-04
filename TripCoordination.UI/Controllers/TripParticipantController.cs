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
    }
}
