using Microsoft.AspNetCore.Mvc;

namespace TripCoordination.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
