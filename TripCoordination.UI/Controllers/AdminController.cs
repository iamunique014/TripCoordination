using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;
using TripCoordination.ViewModel;

namespace TripCoordination.Controllers
{
    public class AdminController : Controller
    {
        //private readonly ILogger<AdminController> _logger;

        private readonly ITownRepository _townRepository;        
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITripRepository _tripRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRoleRepository _UserRoleRepository;
        private readonly IResidenceRepository _residenceRepository;
        private readonly ITripParticipantRepository _tripParticipantRepository;
        private readonly ITripDestinationTownRepository _tripDestinationTownRepository;
        

        public AdminController(/*Logger<AdminController> logger*/ ITownRepository townRepository, IResidenceRepository residenceRepository, IUserRepository userRepository, IProfileRepository profileRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, ITripRepository tripRepository, ITripParticipantRepository tripParticipantRepository, ITripDestinationTownRepository tripDestinationTownRepository)
        {
            //_logger = logger;
            _townRepository = townRepository;
            _residenceRepository = residenceRepository;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _UserRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _tripRepository = tripRepository;
            _tripParticipantRepository = tripParticipantRepository;
            _tripDestinationTownRepository = tripDestinationTownRepository;
        }

        public IActionResult AdminDashboard()
        {
            ViewData["ShowSidebar"] = true;
            return View();
        }
        
        public async Task<IActionResult> Add()
        {
            ViewData["ShowSidebar"] = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Residence residence)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(residence);
                bool addResidence = await _residenceRepository.AddAsync(residence);
                if (addResidence)
                {
                    TempData["msg"] = "Sucessfully Added";
                }
                else
                {
                    TempData["msg"] = "Could not add";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Hebana!! Something went wrong!!!";
            }
            return RedirectToAction(nameof(ManageResidences));
        }
        public async Task<IActionResult> ManageResidences()
        {
            ViewData["ShowSidebar"] = true;
            var residences = await _residenceRepository.GetAllAsync();
            return View(residences);
        }

        //=================================================//
        //                TOWN MANAGEMENT                  //
        //=================================================//

        public async Task<IActionResult> ManageTowns()
        {
            ViewData["ShowSidebar"] = true;
            var towns = await _townRepository.GetAllAsync();
            return View(towns);
        }

        public async Task<IActionResult> EditTown(int townID)
        {
            //Need to get the exact town to edit
            ViewData["ShowSidebar"] = true;
            var town = await _townRepository.GetByIdAsync(townID);
            return View(town);
        }

        [HttpPost]
        public async Task<IActionResult> EditTown(Town town)
        {
            ViewData["ShowSidebar"] = true;
            try
            {
                if (!ModelState.IsValid)
                    return View(town);

                bool updateRecord = await _townRepository.UpdateAsync(town);

                if (updateRecord)
                    TempData["msg"] = "Successfully Added";
                else
                    TempData["msg"] = "Oh Hell Nah";
            }

            catch (Exception ex)
            {
                TempData["msg"] = "Seriously!!!!!";
            }
            return RedirectToAction(nameof(ManageTowns));
        }


        //=================================================//
        //                TRIP MANAGEMENT                  //
        //=================================================//
        //[HttpGet]
        //public async Task<IActionResult> ManageTrips(TripListingViewModelUI model)
        //{
        //    string date = "1/2/1754";

        //    ViewData["ShowSidebar"] = true;
        //    var trip = new Trip
        //    { 

        //        //CreatorUserId = 6,  //CreatorUser = User.Identity.Name, use when authentication is setup
        //        DepartureDate = DateTime.Parse(date)
        //        //TownID = model.DestinationID
        //    };

        //    var trips = await _tripRepository.FindTripsAsync(model, trip);
        //    return View(trips.ToList());
        //}

        [HttpGet]
        public async Task<IActionResult> ManageTrips(TripListingViewModelUI model)
        {
            string date = "1/2/1754";
            ViewData["ShowSidebar"] = true;
            //if (ModelState.IsValid)
            //{

            try
            {
                // map a new Trip to find
                var trip = new Trip
                {
                    //CreatorUserId = 6,  //CreatorUser = User.Identity.Name, use when authentication is setup
                    DepartureDate = DateTime.Parse(date)
                    //TownID = model.DestinationID
                };


                // Find the Trip using repository pattern
                var availableTrips = await _tripRepository.FindTripsAsync(model, trip);

                var viewModel = availableTrips.Select(tripListing => new TripListingViewModelUI
                {
                    TripID = tripListing.TripID,
                    DestinationID = tripListing.DestinationID,
                    Name = tripListing.Surname + " " + tripListing.Name,
                    Surname = tripListing.Surname,
                    DestinationName = tripListing.DestinationName,
                    DepartureDate = tripListing.DepartureDate,
                    Seats = tripListing.Seats
                    // Map additional properties here
                }).ToList();

                //return RedirectToAction("TripListing");
                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging purposes.
                //_logger.LogError(ex, "Error occurred while creating trip.");
                // Add a generic error message to the ModelState.
                ModelState.AddModelError("", "An error occurred while creating the trip. Please try again.");
                Console.WriteLine(ex.ToString());
            }

            //}




            ////log ModelState errors for additional debugging
            //foreach (var state in ModelState)
            //{
            //    foreach (var error in state.Value.Errors)
            //    {
            //        //_logger.LogDebug($"ModelState error in '{state.Key}': {error.ErrorMessage}");
            //        Console.WriteLine("{0}", error.ErrorMessage);
            //    }
            //}

            //// Reloading available towns if the model state is invalid
            //var towns = await _townRepository.GetAllAsync();
            //model.AvailableTowns = towns.Select(t => new SelectListItem
            //{
            //    Value = t.TownID.ToString(),
            //    Text = t.Name
            //}).ToList();

            return View(new List<TripListingViewModelUI>());
        }

        public async Task<IActionResult> CreateTrip()
        {
            ViewData["ShowSidebar"] = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip(Trip trip)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(trip);
                bool addTrip = await _tripRepository.AddAsync(trip);
                if (addTrip)
                {
                    TempData["msg"] = "Sucessfully Added";
                }
                else
                {
                    TempData["msg"] = "Could not add";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Hebana!! Something went wrong!!!";
            }
            return RedirectToAction(nameof(ManageResidences));
        }
    }
}
