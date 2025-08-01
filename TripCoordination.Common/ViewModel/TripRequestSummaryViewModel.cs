using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class TripRequestSummaryViewModel
    {
        public int TripRequestID { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public DateTime DesiredDate { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime RequestedAt { get; set; }

        public string Status
        {
            get
            {
                return IsApproved == null ? "Pending"
                     : IsApproved == true ? "Accepted"
                     : "Declined";
            }
        }
    }
}
