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
            var towns = await _townRepository.GetAllAsync();

            ViewBag.Destination = towns;

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

        //[HttpPost]
        //public async Task<IActionResult> TripListing(TripListingViewModel model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            // Create a new Trip record
        //            var trip = new Trip
        //            {
        //                //CreatorUserId = 1,  //CreatorUser = User.Identity.Name, use when authentication is setup
        //                DepartureDate = model.DepartureDate,
        //                //IsFull = 0,
        //                TownID = model.DestinationID

        //            };

        //            Console.WriteLine("Fetching trip");
        //            // Save the Trip using repository pattern
        //            var availableTrips = await _tripRepository.FindTripsAsync(model, trip);

                   
        //            return View(availableTrips);
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log the exception details for debugging purposes.
        //            _logger.LogError(ex, "Error occurred while creating trip.");
        //            // Add a generic error message to the ModelState.
        //            ModelState.AddModelError("", "An error occurred while creating the trip. Please try again.");
        //            Console.WriteLine(ex.ToString());
        //        }
        //    }

        //    //log ModelState errors for additional debugging
        //    foreach (var state in ModelState)
        //    {
        //        foreach (var error in state.Value.Errors)
        //        {
        //            _logger.LogDebug($"ModelState error in '{state.Key}': {error.ErrorMessage}");
        //            Console.WriteLine("{0}", error.ErrorMessage);
        //        }
        //    }

        //    // Reloading available towns if the model state is invalid
        //    var towns = await _townRepository.GetAllAsync();
        //    model.AvailableTowns = towns.Select(t => new SelectListItem
        //    {
        //        Value = t.TownID.ToString(),
        //        Text = t.Name
        //    }).ToList();

        //    //return View(model);
        //    return RedirectToAction("IndexHome");

        //}

        public IActionResult Login()
        {
            return View();
        }

        //public async Task<IActionResult> TripListing()
        //{
        //    var towns = await _townRepository.GetAllAsync();

        //    ViewBag.Destination = towns;

        //    var townSelectList = towns.Select(t => new SelectListItem
        //    {
        //        Value = t.TownID.ToString(),
        //        Text = t.Name
        //    }).ToList();



        //    var viewModel = new TripListingViewModel
        //    {
        //        AvailableTowns = townSelectList,
        //        //DepartureDate = DateTime.Now
        //    };

        //    return View(viewModel);
        //}

        public IActionResult TripDetails()
        {
            return View();
        }

        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            Console.WriteLine("Posting");
            try
            {
                Console.WriteLine("trying");
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("Not valid");
                    return View(user);
                }  
                else
                {
                    Console.WriteLine("valid");
                }
                    
                Console.WriteLine("testing bool");
                bool addUser = await _userRepository.AddAsync(user);
                Console.WriteLine("bool tested");
                if (addUser)
                {
                    Console.WriteLine("Sucessfully Added");
                    TempData["msg"] = "Sucessfully Added";
                }
                else
                {
                    Console.WriteLine("Could not add");
                    TempData["msg"] = "Could not add";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hebana!! Something went wrong!!!");
                TempData["msg"] = "Hebana!! Something went wrong!!!";
            }
            Console.WriteLine("Returning");
            return RedirectToAction(nameof(DisplayAll));
        }

        public async Task<IActionResult> DisplayAll()
        {

            var users = await _userRepository.GetAllAsync();
            return View(users);
        }





        public IActionResult CreateTrip()
        {
            return View();
        }
        public IActionResult ViewTrip()
        {
            return View();
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
