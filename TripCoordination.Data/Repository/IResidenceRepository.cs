using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public interface IResidenceRepository
    {
        Task<bool> AddAsync(Residence residence);
        Task<bool> UpdateAsync(Residence residence);
        Task<bool> DeleteAsync(int id);

        Task<Residence> GetByIdAsync(int id);

        Task<IEnumerable<Residence>> GetAllAsync();
    }
}
