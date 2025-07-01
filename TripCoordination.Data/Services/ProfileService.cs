using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.Repository;

namespace TripCoordination.Data.Services
{
    // ProfileService.cs
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepo;

        public ProfileService(IProfileRepository profileRepo)
        {
            _profileRepo = profileRepo;
        }

        public async Task<bool> HasProfileAsync(string userID)
        {
            var profile = await _profileRepo.GetUserProfileAsync(userID);
            return profile != null;
        }
    }
}
