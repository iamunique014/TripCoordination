using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;

namespace TripCoordination.Data.Repository
{
    public interface IStudentDashboardRepository
    {
        Task<UpcomingTripViewModel?> GetNextUpcomingTrip(string userID);
        Task<IEnumerable<TripRequestSummaryViewModel>> GetRecentTripRequests(string userID);
    }
}
