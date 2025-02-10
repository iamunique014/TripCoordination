using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public interface ITripDestinationTownRepository
    {
        Task<bool> AddAsync(TripDestinationTown tripDestinationTown, Trip trip);
        Task<bool> UpdateAsync(TripDestinationTown tripDestinationTown);
        Task<bool> DeleteAsync(int id);

        Task<TripDestinationTown> GetByIdAsync(int id);

        Task<IEnumerable<TripDestinationTown>> GetAllAsync();
    }
}
