using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public interface IUserRepository
    {
        Task<bool> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);

        Task<User> GetByIdAsync(int id);
        Task<UserWithRoleViewModel> GetUserWithRole(string id);

        Task<IEnumerable<UserWithRoleViewModel>> GetAllAsync();

        Task<IEnumerable<MyTripGroupedViewModel>> GetUserJoinedTrips(string userId);

    }
}
