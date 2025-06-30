using Microsoft.AspNetCore.Mvc;

namespace TripCoordination.Controllers
{
    public class TripDestinationTownController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
