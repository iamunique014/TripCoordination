using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.DataAccess;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public class UserRoleRepository: IUserRoleRepository
    {
        private readonly ISqlDataAccess _db;

        public UserRoleRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(UserRole userRole)
        {
            try
            {

                await _db.SaveData("sp_Create_UserRole", new { userRole.RoleID, userRole.UserID });
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
                await _db.SaveData("sp_Delete_UserRole", new { ID = id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            string query = "sp_Get_UserRoles";
            return await _db.GetData<UserRole, dynamic>(query, new { });
        }

        public async Task<UserRole> GetByIdAsync(int id)
        {
            IEnumerable<UserRole> result = await _db.GetData<UserRole, dynamic>("sp_Get_UserRole", new { ID = id });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(UserRole userRole)
        {
            try
            {
                await _db.SaveData("sp_Update_UserRole", userRole);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
