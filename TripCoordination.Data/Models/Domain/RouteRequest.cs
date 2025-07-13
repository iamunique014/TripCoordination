using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Data.Models.Domain
{
    public class RouteRequest
    {
        public int RouteRequestID { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string? Reason { get; set; }
        public string UserID { get; set; }
        public DateTime RequestedAt { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }
    }
}
