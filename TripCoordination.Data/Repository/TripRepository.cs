using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.DataAccess;
using TripCoordination.Data.Models.Domain;
using TripCoordination.ViewModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace TripCoordination.Data.Repository
{
    public class TripRepository : ITripRepository
    {
        private readonly ISqlDataAccess _db;

        public TripRepository(ISqlDataAccess db)
        {
            _db = db;
        }
        public int CreateTripAsync(CreateTripViewModel createTripViewModel)
        {
            try
            {
                var parameters = new 
                {
                    createTripViewModel.CreatorUserID,
                    createTripViewModel.RouteID,
                    createTripViewModel.DepartureDate,
                    createTripViewModel.IsFull,
                    createTripViewModel.Seats,
                };


                //return await _db.GetData<TripViewModel, dynamic>(query, new { });
                return _db.GetData<int, dynamic>("sp_Create_Trip", parameters
                ).Result.First();
              
            }
            catch (Exception ex)
            {
                Console.WriteLine("Entered Exception \n");
                Console.WriteLine(ex.ToString());
                return 0;
            }
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

        public async Task<bool> SoftDeleteTripAsync(int tripID)
        {
            try
            {
                await _db.SaveData("sp_SoftDelete_Trip", new { TripID = tripID });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during soft delete: " + ex.Message);
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

        public async Task<IEnumerable<TripViewModel>> GetAllUserTripsAsync(string userID)
        {
            try
            {
                string query = "sp_Get_All_User_Trips";
                return await _db.GetData<TripViewModel, dynamic>(query, new { userID });
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
                var result = await _db.GetData<TripFlatRow, dynamic>(query, new { tripID });

                var trips = result.GroupBy(r => r.TripID).Select(g => new TripWithDestinationsViewModel
                {
                    TripID = g.Key,
                    DepartureDate = g.First().DepartureDate,
                    CreatorName = $"{g.First().CreatorName} {g.First().CreatorSurname}",
                    FromLocation = g.First().FromLocation,
                    ToLocation = g.First().ToLocation,
                    IsFull = g.First().IsFull,
                    Seats = g.First().Seats,
                    Destinations = g
                        .Where(d => d.DestinationID != null)
                            .Select(d => new TripDestinationViewModel
                            {
                                TripDestinationTownID = d.TripDestinationTownID,
                                TownID = d.DestinationID ?? 0,
                                DestinationName = d.DestinationName
                            }).ToList()
                }).ToList();


                return trips;

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
                string query = "sp_Find_Trips_With_Destinations";

                var result = await _db.GetData<TripFlatRow, dynamic>(query, new 
                             {   
                                tripListing.FromLocation,
                                trip.TownID, 
                                trip.DepartureDate 
                             });

                var trips = result.GroupBy(r => r.TripID).Select(g => new TripListingViewModel
                {
                    TripID = g.Key,
                    DepartureDate = g.First().DepartureDate,
                    CreatorName = $"{g.First().CreatorName} {g.First().CreatorSurname}",
                    DestinationID = (int)g.First().DestinationID,
                    DestinationName = g.First().DestinationName,
                    Seats = g.First().Seats,
                    Destinations = g
                        .Where(d => d.DestinationID != null)
                            .Select(d => new TripDestinationViewModel
                            {
                                TripDestinationTownID = d.TripDestinationTownID,
                                TownID = d.DestinationID ?? 0,
                                DestinationName = d.DestinationName
                            }).ToList()
                }).ToList();


                return trips;


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

        public async Task<bool> AddTripStop(TripStop tripStop)
        {
            try
            {
                await _db.SaveData("sp_AddTripStop", new
                {
                    tripStop.TripID,
                    tripStop.TownID,
                    tripStop.Price
                });
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Entered Exception \n");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
