using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCoordination.Common.ViewModel;
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
                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine("haibo kwenzeke nton ?, Mhlawumbi ezincukraca zingakunceda: \n {0}", ex);
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
        public async Task<IEnumerable<UserWithRoleViewModel>> GetAllAsync()
        {
            string query = "sp_Get_Users";
            return await _db.GetData<UserWithRoleViewModel, dynamic>(query, new { });
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
        public async Task<IEnumerable<MyTripGroupedViewModel>> GetUserJoinedTrips(string userId)
        {
            try
            {
                var flatResult = await _db.GetData<MyTripFlatRow, dynamic>("sp_Get_JoinedTrips_By_User", new { UserID = userId });

                var grouped = flatResult.GroupBy(t => t.TripID).Select(g => new MyTripGroupedViewModel
                {
                    TripID = g.Key,
                    TripParticipantID = g.First().TripParticipantID,
                    DepartureDate = g.First().DepartureDate,
                    OrganizerName = g.First().OrganizerName,
                    OrganizerSurname = g.First().OrganizerSurname,
                    PickUpPoint = g.First().PickUpPoint,
                    SeatNumber = g.First().SeatNumber,
                    Destinations = g.Select(d => new TripDestinationViewModel
                    {
                        TownID = d.TownID,
                        DestinationName = d.DestinationName
                    }).DistinctBy(x => x.TownID).ToList()
                });

                return grouped;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Entered Exception \n");
                Console.WriteLine(ex.ToString());
                return Enumerable.Empty<MyTripGroupedViewModel>();
            }           
        }

        public async Task<UserWithRoleViewModel> GetUserWithRole(string id)
        {
            IEnumerable<UserWithRoleViewModel> result = await _db.GetData<UserWithRoleViewModel, dynamic>("sp_Get_User_with_Role", new { UserID = id });
            return result.FirstOrDefault();
        }
    }
}
