using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.DataAccess;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public class TripParticipantRepository : ITripParticipantRepository
    {

        private readonly ISqlDataAccess _db;

        public TripParticipantRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(TripParticipant tripParticipant)
        {
            try
            {

                await _db.SaveData("sp_Create_TripParticipant", new { tripParticipant.TripID, tripParticipant.DestinationTownID, tripParticipant.UserID, tripParticipant.PickUpPoint, tripParticipant.SeatNumber });
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _db.SaveData("sp_Delete_TripParticipant", new { ID = id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<TripParticipant>> GetAllAsync()
        {
            string query = "sp_Get_TripParticipantS";
            return await _db.GetData<TripParticipant, dynamic>(query, new { });
        }

        public async Task<TripParticipant> GetByIdAsync(int id)
        {
            IEnumerable<TripParticipant> result = await _db.GetData<TripParticipant, dynamic>("sp_Get_TripParticipant", new { ID = id });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(TripParticipant tripParticipant)
        {
            try
            {
                await _db.SaveData("sp_Update_TripParticipant", tripParticipant);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<TripParticipantViewModel>> GetParticipantsByTripIDAsync(int tripID)
        {
            string query = "sp_Get_TripParticipants_By_TripID";
            return await _db.GetData<TripParticipantViewModel, dynamic>(query, new { tripID});
        }
    }
}
