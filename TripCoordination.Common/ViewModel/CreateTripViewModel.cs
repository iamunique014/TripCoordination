using System.ComponentModel.DataAnnotations;

namespace TripCoordination.Common.ViewModel
{
    public class CreateTripViewModel
    {
        [Required]
        [Display(Name = "Departure Date and Time")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "Please select trip destination.")]
        public int DestinationID { get; set; }
        [Required(ErrorMessage = "Please enter number of seats.")]
        public int Seats { get; set; }

        public bool IsFull { get; set; }

        // Multi-select value: list of selected town IDs
        [Required(ErrorMessage = "Please select at least one drop-off location.")]
        public List<int> SelectedTownIds { get; set; } = new List<int>();
    }
}
