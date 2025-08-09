using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class AdminDashboardViewModel
    {
        public UserStatsViewModel UserStats { get; set; }
        public TripStatsViewModel TripStats { get; set; }
        public IEnumerable<RecentActivityViewModel> RecentActivities { get; set; }

        public string UserRolesChartJson { get; set; } // For Chart.js
    }

}
