using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TripCoordination.Common.ViewModel;

namespace TripCoordination.ViewModel
{
    public class CreateTripViewModelUI : CreateTripViewModel
    {
        [ValidateNever]
        public IEnumerable<SelectListItem> AvailableTowns { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> AvailableRoutes { get; set; }

        // instead of only IDs, we track town+price
        [ValidateNever]
        public List<TripTownPriceViewModel> SelectedTowns { get; set; } = new();
    }
}
