using Microsoft.AspNetCore.Mvc;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.Models.Domain;
using TripCoordination.Data.Repository;

namespace TripCoordination.Controllers
{
    public class RouteController : Controller
    {
        private readonly IRouteRepository _routeRepository;

        public RouteController(IRouteRepository routeRepo)
        {
            _routeRepository = routeRepo;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["ShowSidebar"] = true;
            var routes = await _routeRepository.GetAllAsync();
            return View(routes);
        }

        public async Task<IActionResult> Details(int routeID)
        {
            ViewData["ShowSidebar"] = true;
            var route = await _routeRepository.GetByIDAsync(routeID);
            if (route == null)
            {
                TempData["Error"] = "Route not found.";
                return RedirectToAction("Index");
            }
            return View(route);
        }

        public IActionResult Create()
        {
            ViewData["ShowSidebar"] = true;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRouteViewModel model)
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
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Failed to create route.";
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int routeID)
        {
            ViewData["ShowSidebar"] = true;
            var route = await _routeRepository.GetByIDAsync(routeID);
            if (route == null)
            {
                TempData["Error"] = "Route not found.";
                return RedirectToAction("Index");
            }
            return View(route);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateRouteViewModel model)
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
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Failed to update route.";
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int routeID)
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
            return RedirectToAction("Index");
        }
    }
}
