using Microsoft.AspNetCore.Mvc;
using TripCoordination.Data.Repository;

namespace TripCoordination.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ViewUsers()
        {
            ViewData["ShowSidebar"] = true;
            var users = await _userRepository.GetAllAsync();
            return View(users);
        }
    }
}
