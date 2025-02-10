using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public interface IRoleRepository
    {
        Task<bool> AddAsync(Role role);
        Task<bool> UpdateAsync(Role role);
        Task<bool> DeleteAsync(int id);

        Task<Role> GetByIdAsync(int id);

        Task<IEnumerable<Role>> GetAllAsync();
    }
}
