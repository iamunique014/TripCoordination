using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.DataAccess;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public class ResidenceRepository : IResidenceRepository
    {

        private readonly ISqlDataAccess _db;

        public ResidenceRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(Residence residence)
        {
            try
            {

                await _db.SaveData("sp_Create_Residence", new { residence.Name, residence.Address });
                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine("haibo kwenzeke nton ?, Mhlawumbi ezincukaca zingakunceda: \n {0}", ex);
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int residenceID)
        {
            try
            {
                await _db.SaveData("sp_Delete_Residence", new { ResidenceID = residenceID });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<Residence>> GetAllAsync()
        {
            string query = "sp_Get_Residences";
            return await _db.GetData<Residence, dynamic>(query, new { });
        }

        public async Task<Residence> GetByIdAsync(int residenceID)
        {
            IEnumerable<Residence> result = await _db.GetData<Residence, dynamic>("sp_Get_Residence", new { ResidenceID = residenceID });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(Residence residence)
        {
            try
            {
                await _db.SaveData("sp_Update_Residence", residence);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
