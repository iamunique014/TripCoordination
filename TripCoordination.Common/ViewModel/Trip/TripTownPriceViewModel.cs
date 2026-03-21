using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class TripTownPriceViewModel
    {
        public int TownID { get; set; }
        public string TownName { get; set; }
        public decimal Price { get; set; }
    }
}
