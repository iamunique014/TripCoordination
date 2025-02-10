using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public interface IUserRepository
    {
        Task<bool> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);

        Task<User> GetByIdAsync(int id);

        Task<IEnumerable<User>> GetAllAsync();
    }
}
