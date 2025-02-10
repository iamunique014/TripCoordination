using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public interface IProfileRepository
    {
        Task<bool> AddAsync(Profile profile);
        Task<bool> UpdateAsync(Profile profile);
        Task<bool> DeleteAsync(int id);

        Task<Profile> GetByIdAsync(int id);

        Task<IEnumerable<Profile>> GetAllAsync();
    }
}
