using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class TripDetailsViewModel
    {
        public int TripID { get; set; }
        public int UserID { get; set; }
        public DateTime DepartureDate { get; set; }
        public int Seats { get; set; }
        public string CreatorUserName { get; set; }
        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }
        public string JoiningUserName { get; set; }
        public string JoiningFirstName { get; set; }
        public string JoiningLastName { get; set; }
        public string Email { get; set; }
        public string PickUpPoint { get; set; }
        public int TownID { get; set; }
        public string DestinationName { get; set; }
    }
}
