using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.Models.Data;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;
using TripCoordination.ViewModel;


namespace TripCoordination.Controllers
{
    [Authorize]
    public class TripController : Controller
    {
        private readonly ILogger<TripController> _logger;

        private readonly ITripRepository _tripRepository;
        private readonly ITownRepository _townRepository;
        private readonly ITripDestinationTownRepository _tripDestinationTownRepository;
        private readonly IResidenceRepository _residenceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRoleRepository _UserRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ApplicationDbContext _context;
       

        public TripController(ILogger<TripController> logger, ApplicationDbContext context,ITripRepository tripRepository, ITownRepository townRepository, ITripDestinationTownRepository tripDestinationTownRepository)
        {
            _logger = logger;
            _tripRepository = tripRepository;
            _townRepository = townRepository;
            _tripDestinationTownRepository = tripDestinationTownRepository;
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> JoinTrip()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> TripListing(TripListingViewModelUI model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // map a new Trip to find
                    var trip = new Trip
                    {
                        //CreatorUserId = 6,  //CreatorUser = User.Identity.Name, use when authentication is setup
                        DepartureDate = model.DepartureDate,
                        TownID = model.DestinationID
                    };


                    // Find the Trip using repository pattern
                    var availableTrips = await _tripRepository.FindTripsAsync(model, trip);

                    var viewModel = availableTrips.Select(tripListing => new TripListingViewModelUI
                    {
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
                    _logger.LogError(ex, "Error occurred while creating trip.");
                    // Add a generic error message to the ModelState.
                    ModelState.AddModelError("", "An error occurred while creating the trip. Please try again.");
                    Console.WriteLine(ex.ToString());
                }
            }

            //log ModelState errors for additional debugging
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    _logger.LogDebug($"ModelState error in '{state.Key}': {error.ErrorMessage}");
                    Console.WriteLine("{0}", error.ErrorMessage);
                }
            }

            // Reloading available towns if the model state is invalid
            var towns = await _townRepository.GetAllAsync();
            model.AvailableTowns = towns.Select(t => new SelectListItem
            {
                Value = t.TownID.ToString(),
                Text = t.Name
            }).ToList();

            return View(new List<TripListingViewModelUI>());
        }

        public IActionResult TripDetails()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> CreateTrip()
        {
            ViewData["ShowSidebar"] = true;

            var towns = await _townRepository.GetAllAsync();

            ViewBag.Destination = towns;
            
            var townSelectList = towns.Select(t => new SelectListItem
            {
                Value = t.TownID.ToString(),
                Text = t.Name
            }).ToList();

            

            var viewModel = new CreateTripViewModelUI
            {
                AvailableTowns = townSelectList,
                DepartureDate = DateTime.Now 
            };

            return View(viewModel);
           
        }



        [HttpPost]
        public async Task<IActionResult> CreateTrip(CreateTripViewModelUI model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var townIds = string.Join(",", model.SelectedTownIds);

                    // Define the stored procedure name and parameters
                    var storedProc = "[dbo].[sp_Create_Trip_With_Destinations]";
                    var parameters = new[]
                    {
                        new SqlParameter("@CreatorUserID", userId),
                        new SqlParameter("@DepartureDate", model.DepartureDate),
                        new SqlParameter("@IsFull", false),
                        new SqlParameter("@Seats", model.Seats),
                        new SqlParameter("@TownIDs", townIds)
                    };

                    // Execute the stored procedure
                    var result = await _context.Database.ExecuteSqlRawAsync(
                        $"EXEC {storedProc} @CreatorUserID, @DepartureDate, @IsFull, @Seats, @TownIDs",
                        parameters
                    );

                    // Handle the result as needed
                    // For example, redirect to the ManageTrips action
                    return RedirectToAction("ManageTrips");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating trip.");
                    ModelState.AddModelError("", "An error occurred while creating the trip. Please try again.");
                }
            }

            // Reload available towns if the model state is invalid
            var towns = await _townRepository.GetAllAsync();
            model.AvailableTowns = towns.Select(t => new SelectListItem
            {
                Value = t.TownID.ToString(),
                Text = t.Name
            }).ToList();

            return View(model);
        }




        //[HttpPost]
        //public async Task<IActionResult> CreateTrip(CreateTripViewModelUI model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            // Create a new Trip record
        //            var trip = new Trip
        //            {
        //                CreatorUserId =  User.FindFirstValue(ClaimTypes.NameIdentifier), //User.Identity.GetUserId(), //use when authentication is setup
        //                DepartureDate = model.DepartureDate,
        //                Seats = model.Seats,
        //                IsFull = 0,

        //            };

        //            // Save the Trip using repository pattern
        //            await _tripRepository.AddAsync(trip);

        //            // For each selected town, create a TripDestinationTown record
        //            foreach (var townId in model.SelectedTownIds)
        //            {
        //                var tripDestination = new TripDestinationTown
        //                {
        //                    TownID = townId
        //                };

        //                Console.WriteLine(tripDestination.TripID.ToString());
        //                await _tripDestinationTownRepository.AddAsync(tripDestination, trip);
        //            }

        //           // return RedirectToAction("ManageTrips");
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

        //    return View(model);

        //}

    }
}
