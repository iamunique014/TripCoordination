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
                   new { },
                   "Default"
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
                     "sp_Admin_GetTripStats",
                     new { },
                     "Default"
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
                    "sp_Admin_GetUserStats",
                    new { },
                    "Default"
                );

                return result.FirstOrDefault() ?? new UserStatsViewModel();
            }
            catch (Exception)
            {
                return new UserStatsViewModel();
            }
        }
    }
}
