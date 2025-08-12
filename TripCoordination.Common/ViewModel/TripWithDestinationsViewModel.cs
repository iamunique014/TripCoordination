using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class TripWithDestinationsViewModel
    {
        public int TripID { get; set; }
        public DateTime DepartureDate { get; set; }
        public string CreatorName { get; set; }
        public string CreatorSurname { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public bool IsFull { get; set; }
        public int Seats { get; set; }

        public List<TripDestinationViewModel> Destinations { get; set; } = new();
    }
}
