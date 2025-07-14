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
        Task<IEnumerable<TripRoute>> GetAllAsync();
        Task<TripRoute?> GetByIDAsync(int routeID);
        Task<bool> CreateAsync(TripRoute route);
        Task<bool> UpdateAsync(TripRoute route);
        Task<bool> SoftDeleteAsync(int routeID);
    }
}
