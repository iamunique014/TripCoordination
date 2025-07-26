using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public interface IProfileRepository
    {
        Task<bool> AddAsync(CreateProfileViewModel profile);
        Task<bool> UpdateAsync(Profile profile);
        Task<bool> DeleteAsync(string id);
        Task<Profile> GetUserProfileAsync(string userID);

        Task<IEnumerable<Profile>> GetAllAsync();
    }
}
