using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Data.Models.Domain
{
    public class TripDestinationTown
    {
        [Key]
        public int DestinationTownID { get; set; }
        public int TripID { get; set; }
        public int TownID { get; set; }
    }
}
