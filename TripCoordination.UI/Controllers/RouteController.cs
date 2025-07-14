using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;

namespace TripCoordination.Controllers
{
    [Authorize]
    public class RouteController : Controller
    {
        private readonly IRouteRepository _routeRepository;

        public RouteController(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<IActionResult> ViewRoutes()
        {
            ViewData["ShowSidebar"] = true;
            var routes = await _routeRepository.GetAllAsync();
            return View(routes);
        }

        public async Task<IActionResult> RouteDetails(int routeID)
        {
            ViewData["ShowSidebar"] = true;
            var route = await _routeRepository.GetByIDAsync(routeID);
            if (route == null)
            {
                TempData["Error"] = "Route not found.";
                return RedirectToAction("ViewRoutes");
            }
            return View(route);
        }
        [Authorize(Roles = "Admin, Organizer")]
        public IActionResult CreateRoute()
        {
            ViewData["ShowSidebar"] = true;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRoute(CreateRouteViewModel model)
        {
            ViewData["ShowSidebar"] = true;
            if (ModelState.IsValid)
            {
                var route = new TripRoute
                {
                    Description = model.Description,
                    FromLocation = model.FromLocation,
                    ToLocation = model.ToLocation
                };

                var success = await _routeRepository.CreateAsync(route);
                if (success)
                {
                    TempData["Success"] = "Route created successfully.";
                    return RedirectToAction("ViewRoutes");
                }
                TempData["Error"] = "Failed to create route.";
            }
            return View(model);
        }
        [Authorize(Roles = "Admin, Organizer")]
        public async Task<IActionResult> EditRoute(int routeID)
        {
            ViewData["ShowSidebar"] = true;
            var route = await _routeRepository.GetByIDAsync(routeID);
            if (route == null)
            {
                TempData["Error"] = "Route not found.";
                return RedirectToAction("ViewRoutes");
            }
            return View(route);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRoute(CreateRouteViewModel model)
        {
            ViewData["ShowSidebar"] = true;
            if (ModelState.IsValid)
            {
                var route = new TripRoute
                {
                    Description = model.Description,
                    FromLocation = model.FromLocation,
                    ToLocation = model.ToLocation
                };

                var success = await _routeRepository.UpdateAsync(route);
                if (success)
                {
                    TempData["Success"] = "Route updated successfully.";
                    return RedirectToAction("ViewRoutes");
                }
                TempData["Error"] = "Failed to update route.";
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRoute(int routeID)
        {
            ViewData["ShowSidebar"] = true;
            var success = await _routeRepository.SoftDeleteAsync(routeID);
            if (success)
            {
                TempData["Success"] = "Route deleted successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to delete route.";
            }
            return RedirectToAction("ViewRoutes");
        }
    }
}
