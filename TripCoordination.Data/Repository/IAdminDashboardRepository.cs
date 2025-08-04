using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;

namespace TripCoordination.Data.Repository
{
    public interface IAdminDashboardRepository
    {
        Task<TripStatsViewModel> GetTripStats();
        Task<UserStatsViewModel> GetUserStats();
        Task<IEnumerable<RecentActivityViewModel>> GetRecentActivityViewModels();
    }
}
