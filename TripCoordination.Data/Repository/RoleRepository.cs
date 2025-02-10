using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.DataAccess;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public class RoleRepository :IRoleRepository
    {
        private readonly ISqlDataAccess _db;

        public RoleRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(Role role)
        {
            try
            {

                await _db.SaveData("sp_Create_Role", new { role.RoleName });
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _db.SaveData("sp_Delete_Role", new { ID = id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            string query = "sp_Get_Roles";
            return await _db.GetData<Role, dynamic>(query, new { });
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            IEnumerable<Role> result = await _db.GetData<Role, dynamic>("sp_Get_Role", new { ID = id });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(Role role)
        {
            try
            {
                await _db.SaveData("sp_Update_Role", role);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
