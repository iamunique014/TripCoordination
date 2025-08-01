using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    //Represents the next trip the student has joined.
    public class UpcomingTripViewModel
    {
        public int TripID { get; set; }
        public DateTime DepartureDate { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string PickupPoint { get; set; }
        public string DestinationName { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerSurname { get; set; }
    }
}
