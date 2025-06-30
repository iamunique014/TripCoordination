using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.DataAccess;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public class TripDestinationTownRepository: ITripDestinationTownRepository
    {
        private readonly ISqlDataAccess _db;

        public TripDestinationTownRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(TripDestinationTown tripDestinationTown, Trip trip)
        {
            try
            {

                await _db.SaveData("sp_Create_TripDestinationTown", new { tripDestinationTown.TownID, trip.CreatorUserId, trip.DepartureDate});
                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(tripDestinationTown.TripID.ToString());
                return false;
            }
        }
        public async Task<bool> DeleteDestinationTwonAsync(int tripDestinationTownID)
        {
            try
            {
                await _db.SaveData("sp_Remove_TripDestination", new { ID = tripDestinationTownID });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Entered Exception: ");
                Console.WriteLine("Failed to remove destination: " + ex.ToString());
                return false;
            }
        }
        public async Task<IEnumerable<TripDestinationTown>> GetAllAsync()
        {
            string query = "sp_Get_TripDestinationTwons";
            return await _db.GetData<TripDestinationTown, dynamic>(query, new { });
        }

        public async Task<TripDestinationTown> GetByIdAsync(int id)
        {
            IEnumerable<TripDestinationTown> result = await _db.GetData<TripDestinationTown, dynamic>("sp_Get_TripDestinationTown", new { ID = id });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(TripDestinationTown tripDestinationTown)
        {
            try
            {
                await _db.SaveData("sp_Update_TripDestinationTown", tripDestinationTown);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
