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
        Task<bool> AddAsync(TripRequest request);
        Task<bool> DeleteAsync(int tripRequestID);
        Task<IEnumerable<TripRequest>> GetAllAsync();
        Task<TripRequest?> GetByIdAsync(int tripRequestID);
        Task<bool> ApproveAsync(int tripRequestID);
    }
}
