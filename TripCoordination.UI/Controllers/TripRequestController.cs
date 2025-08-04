using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;

namespace TripCoordination.Controllers
{
    [Authorize]
    public class TripRequestController : Controller
    {
        private readonly ITripRequestRepository _tripRequestRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TripRequestController(
            ITripRequestRepository tripRequestRepository,
            UserManager<ApplicationUser> userManager)
        {
            _tripRequestRepository = tripRequestRepository;
            _userManager = userManager;
        }

        // GET: /TripRequest
        public async Task<IActionResult> ViewRequest()
        {
            ViewData["ShowSideBar"] = true;
            if (User.IsInRole("Student"))
            {
                var user = await _userManager.GetUserAsync(User);
                var userRequests = await _tripRequestRepository.GetAllUserTripRequestAsync(user.Id);
                return View(userRequests);
            }
            var requests = await _tripRequestRepository.GetAllAsync();
            return View(requests);
        }

        // GET: /TripRequest/Create
        public IActionResult CreateRequest()
        {
            ViewData["ShowSideBar"] = true;
            return View();
        }

        // POST: /TripRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRequest(TripRequestViewModel model)
        {
            ViewData["ShowSideBar"] = true;
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            var newRequest = new TripRequest
            {
                FromLocation = model.FromLocation,
                ToLocation = model.ToLocation,
                DesiredDate = model.DesiredDate,
                Notes = model.Notes,
                UserID = user.Id,
                RequestedAt = DateTime.Now,
                IsApproved = false,
                IsDeleted = false
            };

            var success = await _tripRequestRepository.AddAsync(newRequest);
            if (success) 
            {
                TempData["Success"] = "Request Created Successfully!";
                return RedirectToAction("ViewRequest");
            }
                

            ModelState.AddModelError("", "Something went wrong while submitting your request.");
            TempData["Error"] = "Something went wrong while submitting your request.";
            return View(model);
        }

        // POST: /TripRequest/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            ViewData["ShowSideBar"] = true;
            var result = await _tripRequestRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        // POST: /TripRequest/Approve/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveRequest(int id)
        {
            ViewData["ShowSideBar"] = true;
            var result = await _tripRequestRepository.ApproveAsync(id);
            return RedirectToAction("Index");
        }
    }
}
