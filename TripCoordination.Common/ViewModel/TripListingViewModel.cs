using System.ComponentModel.DataAnnotations;

namespace TripCoordination.Common.ViewModel
{
    public class TripListingViewModel
    {
        [Required(ErrorMessage = "Please select a destination")]
        public int DestinationID { get; set; }
        public DateTime DepartureDate { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? DestinationName { get; set; }
        public int Seats { get; set; }

    }
}
