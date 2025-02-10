using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Data.Models.Domain
{
    public class UserRole
    {
        [Key] 
        public int UserID { get; set; }
        [Key]
        public int RoleID { get; set; }
    }
}
