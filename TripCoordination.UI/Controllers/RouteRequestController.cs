using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;

namespace TripCoordination.Controllers
{
    [Authorize]
    public class RouteRequestController : Controller
    {
        private readonly IRouteRequestRepository _routeRequestRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public RouteRequestController(IRouteRequestRepository routeRequestRepo, UserManager<IdentityUser> userManager)
        {
            _routeRequestRepository = routeRequestRepo;
            _userManager = userManager;
        }

        // ADMIN: View All Route Requests
        [Authorize(Roles = "Admin, Organizer")]
        public async Task<IActionResult> ViewRouteRequests()
        {
            ViewData["ShowSideBar"] = true;
            var requests = await _routeRequestRepository.GetAllAsync();
            return View(requests);
        }

        // USER: View Their Own Route Requests
        public async Task<IActionResult> MyRequests()
        {
            ViewData["ShowSideBar"] = true;
            var user = await _userManager.GetUserAsync(User);
            var requests = await _routeRequestRepository.GetAllUserRouteRequestAsync(user.Id);
            return View(requests);
        }

        // GET: Create
        public IActionResult CreateRouteRequest()
        {
            ViewData["ShowSideBar"] = true;
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRouteRequest(RouteRequestViewModel model)
        {
            ViewData["ShowSideBar"] = true;
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);

            var request = new RouteRequest
            {
                FromLocation = model.FromLocation,
                ToLocation = model.ToLocation,
                Reason = model.Reason,
                UserID = user.Id,
                RequestedAt = DateTime.Now,
                IsDeleted = false,
                IsApproved = false
            };

            var result = await _routeRequestRepository.AddAsync(request);

            if (result)
            {
                TempData["Success"] = "Route request submitted successfully.";
                return RedirectToAction(nameof(MyRequests));
            }

            ModelState.AddModelError("", "Something went wrong while submitting your request.");
            TempData["Error"] = "Something went wrong while submitting your request.";
            return View(model);
        }

        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRouteRequest(int id)
        {
            ViewData["ShowSideBar"] = true;
            await _routeRequestRepository.DeleteAsync(id);
            return RedirectToAction(nameof(MyRequests));
        }

        // POST: Approve (Admin only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveRouteRequest(int id)
        {
            ViewData["ShowSideBar"] = true;
            await _routeRequestRepository.ApproveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
