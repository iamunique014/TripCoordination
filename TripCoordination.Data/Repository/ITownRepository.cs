using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public interface ITownRepository
    {
        Task<bool> AddAsync(Town town);
        Task<bool> UpdateAsync(Town town);
        Task<bool> DeleteAsync(int id);

        Task<Town> GetByIdAsync(int townID);

        Task<IEnumerable<Town>> GetAllAsync();
    }
}
