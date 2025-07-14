using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Data.DataAccess;
using TripCoordination.Data.Models.Domain;

namespace TripCoordination.Data.Repository
{

    public class RouteRepository : IRouteRepository
    {
        private readonly ISqlDataAccess _db;

        public RouteRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Route>> GetAllAsync()
        {
            try
            {
                return await _db.GetData<Route, dynamic>("sp_Get_Routes", new { });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllAsync: {ex.Message}");
                return Enumerable.Empty<Route>();
            }
        }

        public async Task<Route?> GetByIDAsync(int routeID)
        {
            try
            {
                var result = await _db.GetData<Route, dynamic>("sp_Get_Route_By_ID", new { routeID });
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetByIDAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateAsync(Route route)
        {
            try
            {
                await _db.SaveData("sp_Create_Route", new
                {
                    route.Description,
                    route.FromLocation,
                    route.ToLocation
                });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Route route)
        {
            try
            {
                await _db.SaveData("sp_Update_Route", new
                {
                    route.RouteID,
                    route.Description,
                    route.FromLocation,
                    route.ToLocation
                });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SoftDeleteAsync(int routeID)
        {
            try
            {
                await _db.SaveData("sp_Soft_Delete_Route", new { routeID });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SoftDeleteAsync: {ex.Message}");
                return false;
            }
        }
    }
}
