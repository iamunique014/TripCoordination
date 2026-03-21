using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public interface ITripParticipantRepository
    {
        Task<bool> AddAsync(TripParticipant tripParticipant);
        Task<bool> UpdateAsync(TripParticipant tripParticipant);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteTripParticipantAsync(int tripParticipantID, int tripID);
        Task<TripParticipant> GetByIdAsync(int id);
        Task<IEnumerable<TripParticipant>> GetAllAsync();
        Task<IEnumerable<TripParticipantViewModel>> GetParticipantsByTripIDAsync(int tripID);
        Task<bool> LeaveTripAsync(int tripParticipantID);
    }
}
