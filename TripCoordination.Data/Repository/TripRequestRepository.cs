using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.DataAccess;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public class TripRequestRepository : ITripRequestRepository
    {
        private readonly ISqlDataAccess _db;

        public TripRequestRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(TripRequest request)
        {
            try
            {
                await _db.SaveData("sp_Add_TripRequest", new
                {
                    request.FromLocation,
                    request.ToLocation,
                    request.DesiredDate,
                    request.Notes,
                    request.UserID
                });

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to add trip request: {0}", ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int tripRequestID)
        {
            try
            {
                await _db.SaveData("sp_Delete_TripRequest", new { TripRequestID = tripRequestID });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<TripRequest>> GetAllAsync()
        {
            return await _db.GetData<TripRequest, dynamic>("sp_Get_All_TripRequests", new { });
        }

        public async Task<TripRequest?> GetByIdAsync(int tripRequestID)
        {
            var result = await _db.GetData<TripRequest, dynamic>("sp_Get_TripRequest_ByID", new { TripRequestID = tripRequestID });
            return result.FirstOrDefault();
        }

        public async Task<bool> ApproveAsync(int tripRequestID)
        {
            try
            {
                await _db.SaveData("sp_Approve_TripRequest", new { TripRequestID = tripRequestID });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<TripRequest>> GetAllUserTripRequestAsync(string userID)
        {
            return await _db.GetData<TripRequest, dynamic>("sp_Get_All_User_TripRequests", new { userID });
        }
    }
}
