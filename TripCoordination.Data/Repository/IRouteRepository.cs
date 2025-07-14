using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public interface IRouteRepository
    {
        Task<IEnumerable<Route>> GetAllAsync();
        Task<Route?> GetByIDAsync(int routeID);
        Task<bool> CreateAsync(Route route);
        Task<bool> UpdateAsync(Route route);
        Task<bool> SoftDeleteAsync(int routeID);
    }
}
