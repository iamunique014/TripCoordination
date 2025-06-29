using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class TripViewModel
    {
        public int TripID { get; set; }
        public string UserID { get; set; }
        public DateTime DepartureDate { get; set; }
        public int Seats { get; set; }
        public string CreatorName { get; set; }
        public string CreatorSurname { get; set; }
        public int IsFull { get; set; }
    }
}
