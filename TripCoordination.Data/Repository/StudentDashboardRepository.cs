using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.DataAccess;

namespace TripCoordination.Data.Repository
{
    public class StudentDashboardRepository: IStudentDashboardRepository
    {
        private readonly ISqlDataAccess _db;

        public StudentDashboardRepository(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<UpcomingTripViewModel?> GetNextUpcomingTrip(string userID)
        {
            var results = await _db.GetData<UpcomingTripViewModel, dynamic>(
                "sp_GetNextUpcomingTrip",
                new { UserID = userID }
            );

            return results.FirstOrDefault();
        }
        public async Task<IEnumerable<TripRequestSummaryViewModel>> GetRecentTripRequests(string userID)
        {
            return await _db.GetData<TripRequestSummaryViewModel, dynamic>(
                "sp_GetRecentTripRequests",
                new { UserID = userID }
            );
        }

        public async Task<StudentTripStatsViewModel?> GetStudentTripStats(string userID)
        {
            var result = await _db.GetData<StudentTripStatsViewModel, dynamic>(
                "sp_GetStudentTripStats", 
                new { UserID = userID }
            );

            return result.FirstOrDefault();
        }
    }
}
