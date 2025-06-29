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
    public class TripRepository : ITripRepository
    {
        private readonly ISqlDataAccess _db;

        public TripRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(Trip trip)
        {
            try
            {

                await _db.SaveData("sp_Create_Trip", new
                { 
                    trip.CreatorUserId, 
                    trip.DepartureDate, 
                    trip.IsFull, 
                    trip.Seats 
                });

                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Entered Exception \n");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _db.SaveData("sp_Delete_Trip", new { ID = id });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Entered Exception \n");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public async Task<IEnumerable<TripViewModel>> GetAllAsync()
        {
            try
            {
                string query = "sp_GetAll_Trips";
                return await _db.GetData<TripViewModel, dynamic>(query, new { });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Entered Exception \n");
                Console.WriteLine(ex.ToString());
                return Enumerable.Empty<TripViewModel>();
            }
        }

        public async Task<IEnumerable<TripWithDestinationsViewModel>> GetTripWithDestinations(int tripID)
        {
            try
            {
                string query = "sp_Find_Trips";
                return await _db.GetData<TripWithDestinationsViewModel, dynamic>(query, new { tripID });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Entered Exception \n");
                Console.WriteLine(ex.ToString());
                return Enumerable.Empty<TripWithDestinationsViewModel>();
            }
        }

        public async Task<IEnumerable<TripListingViewModel>> FindTripsAsync(TripListingViewModel tripListing, Trip trip)
        {
            try
            {
                string query = "sp_Find_Trips";

                return await _db.GetData<TripListingViewModel, dynamic>(query, new 
                { 
                    trip.TownID, 
                    trip.DepartureDate 

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Entered Exception \n");
                Console.WriteLine(ex.ToString());
                return Enumerable.Empty<TripListingViewModel>();
            }
        }

        public async Task<Trip> GetByIdAsync(int tripID)
        {
            IEnumerable<Trip> result = await _db.GetData<Trip, dynamic>("sp_Get_Trip", new { TripID = tripID });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(Trip trip)
        {
            try
            {
                await _db.SaveData("sp_Update_Trip", trip);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Entered Exception \n");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<TripDetailsViewModel> FindTripDetails(Trip trip, User user)
        {
                IEnumerable<TripDetailsViewModel> result = await _db.GetData<TripDetailsViewModel, dynamic>("sp_Find_TripDetails", new { trip.TripID, trip.TownID, user.UserID });
                return result.FirstOrDefault();
        }

        public async Task<bool> JoinTrip(TripDetailsViewModel tripDetails, User user)
        {
            try
            {

                await _db.SaveData("sp_Join_Trip", new
                {
                    user.UserID,
                    tripDetails.TripID,
                    tripDetails.TownID,
                    tripDetails.PickUpPoint
                    
                    
                });

                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Entered Exception \n");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
