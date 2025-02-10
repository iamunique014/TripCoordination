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

        Task<Trip> GetByIdAsync(int id);

        Task<IEnumerable<Trip>> GetAllAsync();

        Task<IEnumerable<TripListingViewModel>> FindTripsAsync(TripListingViewModel tripListing, Trip trip);

    }
}
