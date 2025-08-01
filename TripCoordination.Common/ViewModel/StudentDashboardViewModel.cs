using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class StudentDashboardViewModel
    {
        public UpcomingTripViewModel? UpcomingTrip { get; set; }
        public StudentTripStatsViewModel? TripStats { get; set; }
        public IEnumerable<TripRequestSummaryViewModel> RecentTripRequests { get; set; } 
    }
}
