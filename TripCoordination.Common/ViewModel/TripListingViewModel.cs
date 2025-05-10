using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TripCoordination.Common.ViewModel
{
    public class TripListingViewModel
    {
        public int TripID { get; set; }
        [Required(ErrorMessage = "Please select a destination")]
        public int DestinationID { get; set; }
        [Required(ErrorMessage = "Please Enter a pickUpPoint eg. Res Address")]
        public string PickUpLocation { get; set; }
        public DateTime DepartureDate { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? DestinationName { get; set; }
        public int Seats { get; set; }
              
    }
}
