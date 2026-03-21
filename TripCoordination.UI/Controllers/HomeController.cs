using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;
using TripCoordination.UI.Models;
using TripCoordination.ViewModel;

namespace TripCoordination.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITripRepository _tripRepository;
        private readonly ITownRepository _townRepository;
        private readonly IResidenceRepository _residenceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRoleRepository _UserRoleRepository;
        private readonly IRoleRepository _roleRepository;

        public HomeController(ILogger<HomeController> logger, ITripRepository tripRepository, ITownRepository townRepository,IResidenceRepository residenceRepository, IUserRepository userRepository, IProfileRepository profileRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _logger = logger;
            _tripRepository = tripRepository;
            _townRepository = townRepository;
            _residenceRepository = residenceRepository;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _UserRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }


        public async Task<IActionResult> Index()
        {
            ViewData["HideSidebarToggle"] = true;
            var towns = await _townRepository.GetAllAsync();

            ViewBag.Towns = towns;

            var townSelectList = towns.Select(t => new SelectListItem
            {
                Value = t.TownID.ToString(),
                Text = t.Name
            }).ToList();

            var viewModel = new TripListingViewModelUI
            {
                AvailableTowns = townSelectList,
                DepartureDate = DateTime.Today
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
