using Microsoft.AspNetCore.Mvc.Rendering;
using TripCoordination.Common.ViewModel;

namespace TripCoordination.ViewModel
{
    public class CreateTripViewModelUI : CreateTripViewModel
    {    
        public IEnumerable<SelectListItem>? AvailableTowns { get; set; }
    }
}
