using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class OrganizerDashboardViewModel
    {
        public UpcomingTripViewModel? UpcomingTrip { get; set; }
        public OrganizerTripStatsViewModel? TripStats { get; set; }
        public IEnumerable<TripRequestSummaryViewModel>? RecentTripRequests { get; set; }
    }
}
