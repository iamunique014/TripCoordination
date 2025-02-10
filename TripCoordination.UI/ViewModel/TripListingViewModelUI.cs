using Microsoft.AspNetCore.Mvc.Rendering;
using TripCoordination.Common.ViewModel;

namespace TripCoordination.ViewModel
{
    public class TripListingViewModelUI : TripListingViewModel
    {
        public IEnumerable<SelectListItem>? AvailableTowns { get; set; }
    }
}
