using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public interface ITripRepository
    {
        Task<bool> AddAsync(Trip trip);
        //Task<bool> CreateAsync(CreateTripViewModel createTripViewModel);
        Task<bool> UpdateAsync(Trip trip);
        Task<bool> DeleteAsync(int id);
        Task<bool> SoftDeleteTripAsync(int tripID);
        Task<Trip> GetByIdAsync(int tripID);

        Task<IEnumerable<TripViewModel>> GetAllAsync();
        Task<IEnumerable<TripViewModel>> GetAllUserTripsAsync(string userID);
        Task<IEnumerable<TripWithDestinationsViewModel>> GetTripWithDestinations(int tripID);

        Task<IEnumerable<TripListingViewModel>> FindTripsAsync(TripListingViewModel tripListing, Trip trip);

        Task<TripDetailsViewModel> FindTripDetails(Trip trip, User user);
        Task<bool> JoinTrip(TripDetailsViewModel tripDetails, User user);

    }
}
