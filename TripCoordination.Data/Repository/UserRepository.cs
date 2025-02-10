using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.DataAccess;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ISqlDataAccess _db;

        public UserRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(User user)
        {
            try
            {

                await _db.SaveData("sp_Create_User", new { user.Email, user.PasswordHash });
                Console.WriteLine("Siyazama ukufake luUser yakho");
                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine("haibo kwenzeke nton ?, Mhlawumbi ezincukaca zingakunceda: \n {0}", ex);
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _db.SaveData("sp_Delete_User", new { ID = id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            string query = "sp_Get_Users";
            return await _db.GetData<User, dynamic>(query, new { });
        }

        public async Task<User> GetByIdAsync(int id)
        {
            IEnumerable<User> result = await _db.GetData<User, dynamic>("sp_Get_User", new { ID = id });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(User user)
        {
            try
            {
                await _db.SaveData("sp_Update_User", user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
