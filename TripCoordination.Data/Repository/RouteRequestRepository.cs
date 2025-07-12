using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.DataAccess;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{
    public class RouteRequestRepository : IRouteRequestRepository
    {
        private readonly ISqlDataAccess _db;

        public RouteRequestRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(RouteRequest request)
        {
            try
            {
                await _db.SaveData("sp_Add_RouteRequest", new
                {
                    request.FromLocation,
                    request.ToLocation,
                    request.Reason,
                    request.UserID
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int routeRequestID)
        {
            try
            {
                await _db.SaveData("sp_Delete_RouteRequest", new { RouteRequestID = routeRequestID });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<RouteRequest>> GetAllAsync()
        {
            return await _db.GetData<RouteRequest, dynamic>("sp_Get_All_RouteRequests", new { });
        }

        public async Task<IEnumerable<RouteRequest>> GetAllUserRouteRequestAsync(string userID)
        {
            return await _db.GetData<RouteRequest, dynamic>("sp_Get_All_User_RouteRequest", new { });
        }

        public async Task<RouteRequest?> GetByIdAsync(int routeRequestID)
        {
            var result = await _db.GetData<RouteRequest, dynamic>(
                "sp_Get_RouteRequest_By_Id",
                new { RouteRequestID = routeRequestID });

            return result.FirstOrDefault();
        }

        public async Task<bool> ApproveAsync(int routeRequestID)
        {
            try
            {
                await _db.SaveData("sp_Approve_RouteRequest", new { RouteRequestID = routeRequestID });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
