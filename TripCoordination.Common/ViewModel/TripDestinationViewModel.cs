using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class TripDestinationViewModel
    {
        public int TripDestinationID { get; set; }  // ID from TripDestinationTowns table
        public int TownID { get; set; }
        public string DestinationName { get; set; }
    }
}
