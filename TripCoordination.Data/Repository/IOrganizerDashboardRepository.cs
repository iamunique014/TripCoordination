using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;

namespace TripCoordination.Data.Repository
{
    public interface IOrganizerDashboardRepository
    {
        Task<UpcomingTripViewModel?> GetUpcomingTrip(string userID);
        Task<OrganizerTripStatsViewModel?> GetOrganizerTripStats(string userID);
        Task<IEnumerable<TripRequestSummaryViewModel>?> GetRecentTripRequests();
        Task<IEnumerable<ChartDataPoint>> GetMonthlyTripCountByOrganizer(string userID);
    }
}