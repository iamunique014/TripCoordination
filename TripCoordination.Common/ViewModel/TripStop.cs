using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class TripStop
    {
        public int TripID { get; set; }
        public int TownID { get; set; }
        public string? Price { get; set; }
    }
}
