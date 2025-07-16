using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TripCoordination.Common.ViewModel
{
    public class TripListingViewModel
    {
        public int TripID { get; set; }
        [Required(ErrorMessage = "Please select a destination")]
        public int DestinationID { get; set; }
        [Required(ErrorMessage = "Please select a Departure City eg. Gqeberha")]
        public string FromLocation { get; set; }
        [Required(ErrorMessage = "Please Enter a pickUpPoint eg. Res Address")]
        public DateTime DepartureDate { get; set; }
        public string? CreatorName { get; set; }
        public string? CreatorSurname { get; set; }
        public string? DestinationName { get; set; }
        public int Seats { get; set; }
              
    }
}
