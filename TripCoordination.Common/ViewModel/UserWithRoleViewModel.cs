using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class UserWithRoleViewModel
    {
        public string Id { get; set; }              // AspNetUsers.Id
        public string UserName { get; set; }        // AspNetUsers.UserName
        public string Email { get; set; }           // AspNetUsers.Email
        public string RoleID { get; set; }          // AspNetUserRole.Id
        public string RoleName { get; set; }        // AspNetRoles.Name
        public string FirstName { get; set; }       // Profiles.Name
        public string LastName { get; set; }        // Profiles.Surname
        public string PhoneNumber { get; set; }     // Profiles.PhoneNumber
        public DateTimeOffset Status { get; set; }  // AspNetRoles.LockOutEnabled
    }
}
