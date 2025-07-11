using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public interface ITripRequestRepository
    {
        Task<IEnumerable<TripRequest>> GetAllAsync();
        Task<TripRequest?> GetByIdAsync(int id);
        Task AddAsync(TripRequest request);
        Task DeleteAsync(int id);
        Task ApproveAsync(int id); 
    }
}
