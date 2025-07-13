using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public interface IRouteRequestRepository
    {
        Task<bool> AddAsync(RouteRequest request);
        Task<bool> DeleteAsync(int routeRequestID);
        Task<IEnumerable<RouteRequest>> GetAllAsync();

        Task<IEnumerable<RouteRequest>> GetAllUserRouteRequestAsync(string userID);
        Task<RouteRequest?> GetByIdAsync(int routeRequestID);
        Task<bool> ApproveAsync(int routeRequestID);
    }
}
