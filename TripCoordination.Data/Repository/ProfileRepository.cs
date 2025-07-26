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
    public class ProfileRepository: IProfileRepository
    {
        private readonly ISqlDataAccess _db;

        public ProfileRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(CreateProfileViewModel profile)
        {
            try
            {

                await _db.SaveData("sp_Create_Profile", new 
                { 
                    profile.Title, 
                    profile.Name, 
                    profile.Surname, 
                    profile.Email, 
                    profile.PhoneNumber, 
                    profile.UserID, 
                    profile.DateOfBirth, 
                    profile.ResidenceID 
                });
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                await _db.SaveData("sp_Delete_Profile", new { ID = id });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Entered Exception \n");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public async Task<IEnumerable<Profile>> GetAllAsync()
        {
            string query = "sp_Get_Profiles";
            return await _db.GetData<Profile, dynamic>(query, new { });
        }

        public async Task<Profile> GetByIdAsync(string id)
        {
            IEnumerable<Profile> result = await _db.GetData<Profile, dynamic>("sp_Get_Profile", new { ID = id });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(Profile profile)
        {
            try
            {
                await _db.SaveData("sp_Update_Profile", new
                {
                    profile.Title,
                    profile.Name,
                    profile.Surname,
                    profile.Email,
                    profile.PhoneNumber,
                    profile.UserID,
                    profile.DateOfBirth,
                    profile.ResidenceID
                });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Entered Exception \n");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<CreateProfileViewModel> GetUserProfileAsync(string userID)
        {
            try
            {
                IEnumerable<CreateProfileViewModel> userProfile = await _db.GetData<CreateProfileViewModel, dynamic>("sp_Get_User_Profile", new { userID });
                return userProfile.FirstOrDefault();
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Entered Exception \n");
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
