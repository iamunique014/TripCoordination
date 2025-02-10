using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.DataAccess;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public class TownRepository : ITownRepository
    {
        private readonly ISqlDataAccess _db;

        public TownRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(Town town)
        {
            try
            {

                await _db.SaveData("sp_Create_Town", new { town.Region, town.Name });
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
                await _db.SaveData("sp_Delete_Town", new { ID = id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<Town>> GetAllAsync()
        {
            string query = "sp_Get_Towns";
            return await _db.GetData<Town, dynamic>(query, new { });
        }

        public async Task<Town> GetByIdAsync(int id)
        {
            IEnumerable<Town> result = await _db.GetData<Town, dynamic>("sp_Get_Town", new { ID = id });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(Town town)
        {
            try
            {
                await _db.SaveData("sp_Update_Town", town);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
