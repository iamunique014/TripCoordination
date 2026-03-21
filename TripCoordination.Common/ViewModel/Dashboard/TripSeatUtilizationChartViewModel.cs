using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class TripSeatUtilizationChartViewModel
    {
        public string TripTitle { get; set; }
        public int SeatsFilled { get; set; }
        public int SeatsAvailable { get; set; }
    }
}
