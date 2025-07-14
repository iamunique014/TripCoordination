using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Data.Models.Domain
{
    public class TripRoute
    {
        public int RouteID { get; set; }

        public string Description { get; set; } = string.Empty;

        public string FromLocation { get; set; } = string.Empty;

        public string ToLocation { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
    }
}
