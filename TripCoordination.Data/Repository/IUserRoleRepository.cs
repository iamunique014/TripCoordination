using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public interface IUserRoleRepository
    {
        Task<bool> AddAsync(UserRole userRole);
        Task<bool> UpdateAsync(UserRole userRole);
        Task<bool> DeleteAsync(int id);

        Task<UserRole> GetByIdAsync(int id);

        Task<IEnumerable<UserRole>> GetAllAsync();
    }
}
