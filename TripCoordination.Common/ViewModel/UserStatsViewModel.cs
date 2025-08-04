using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class UserStatsViewModel
    {
        public int TotalUsers { get; set; }
        public int ActiveOrganizers { get; set; }
        public int BlockedUsers { get; set; }
        public int NewUsersThisMonth { get; set; }
    }

}
