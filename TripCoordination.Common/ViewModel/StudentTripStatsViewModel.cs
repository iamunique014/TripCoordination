using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class StudentTripStatsViewModel
    {
        // Quick stats
        public int TotalTripsJoined { get; set; }
        public int TotalTripRequests { get; set; }
        public int TotalRouteRequests { get; set; }
        public int TotalRequestsMade { get; set; }
        public int TripsJoinedThisMonth { get; set; }
    }
}
