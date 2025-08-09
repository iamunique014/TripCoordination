using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.DataAccess;

namespace TripCoordination.Data.Repository
{
    public class OrganizerDashboardRepository : IOrganizerDashboardRepository
    {
        private readonly ISqlDataAccess _db;

        public OrganizerDashboardRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<OrganizerTripStatsViewModel?> GetOrganizerTripStats(string userID)
        {
            try
            {
                var result = await _db.GetData<OrganizerTripStatsViewModel, dynamic>(
                    "sp_GetOganizerStats",
                    new
                    {
                        UserID = userID
                    }
                );

                return result.FirstOrDefault();
            }
            catch(Exception)
            {
                return null;
            }
            
        }
        public async Task<UpcomingTripViewModel?> GetUpcomingTrip(string userID)
        {
            try
            {
                var result = await _db.GetData<UpcomingTripViewModel, dynamic>(
                    "sp_GetOrganizerUpcomingTrip",
                    new { UserID = userID }
                );

                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<TripRequestSummaryViewModel>?> GetRecentTripRequests()
        {
            try
            {
                return await _db.GetData<TripRequestSummaryViewModel, dynamic>(
                    "sp_GetRecentTripRequests", new {}
                    
                );
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ChartDataPoint>> GetMonthlyTripCountByOrganizer(string userID)
        {
            try
            {
                return await _db.GetData<ChartDataPoint, dynamic>("sp_Organizer_GetMonthlyTripCount", new { userID });
            }
            catch
            {
                return null;

            }
        }

        public async Task<IEnumerable<TripSeatUtilizationChartViewModel>> GetTripSeatUtilizationChartData(string userID)
        {
            return await _db.GetData<TripSeatUtilizationChartViewModel, dynamic>(
                "sp_Organizer_GetSeatUtilizationByTrip", 
                new { userID }
            );
        }
    }
}
