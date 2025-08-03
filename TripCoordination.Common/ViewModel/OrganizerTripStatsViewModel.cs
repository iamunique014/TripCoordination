using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class OrganizerTripStatsViewModel
    {
        public int TotalTripsCreated { get; set; }
        public int TripsThisMonth { get; set; }
        public int PendingTripRequests { get; set; }
    }
}
