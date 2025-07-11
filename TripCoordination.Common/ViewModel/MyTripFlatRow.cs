using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class MyTripFlatRow
    {
        public int TripParticipantID { get; set; }
        public int TripID { get; set; }
        public DateTime DepartureDate { get; set; }
        public int Seats { get; set; }
        public bool IsFull { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerSurname { get; set; }
        public int SeatNumber { get; set; }
        public string PickUpPoint { get; set; }

        public int TownID { get; set; }
        public string DestinationName { get; set; }
    }
}
