using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.DataAccess;

namespace TripCoordination.Data.Repository
{
    public class AdminDashboardRepository : IAdminDashboardRepository
    {
        private readonly ISqlDataAccess _db;

        public AdminDashboardRepository(ISqlDataAccess db)
        {
            _db = db;
        }
        public async Task<IEnumerable<RecentActivityViewModel>> GetRecentActivityViewModels()
        {
            try
            {
                var result = await _db.GetData<RecentActivityViewModel, dynamic>(
                   "sp_Admin_GetRecentActivity",
                   new { }
                 
                );

                return result;
            }
            catch
            {
                return Enumerable.Empty<RecentActivityViewModel>();
            }
        }

        public async Task<TripStatsViewModel> GetTripStats()
        {
            try
            {
                var result = await _db.GetData<TripStatsViewModel, dynamic>(
                     "sp_Admin_GetTripOverview",
                     new { }
                );

                return result.FirstOrDefault() ?? new TripStatsViewModel();
            }
            catch (Exception)
            {
                return new TripStatsViewModel();
            }
        }

        public async Task<UserStatsViewModel> GetUserStats()
        {
            try
            {
                var result = await _db.GetData<UserStatsViewModel, dynamic>(
                    "sp_Admin_GetUserSummary",
                    new { }
                );

                return result.FirstOrDefault() ?? new UserStatsViewModel();
            }
            catch (Exception)
            {
                return new UserStatsViewModel();
            }
        }
        public async Task<Dictionary<string, int>> GetUserRoleDistribution()
        {

            var data = await _db.GetData<dynamic, dynamic>("sp_GetUserRoleDistribution", new { });

            return data.ToDictionary(
                d => (string)d.Name,
                d => (int)d.Count
            );
        }
        public async Task<IEnumerable<ChartDataPoint>> GetTripsCreatedByMonthAsync()
        {
            return await _db.GetData<ChartDataPoint, dynamic>("sp_Admin_GetTripsCreatedByMonth", new { });
        }

    }
}
