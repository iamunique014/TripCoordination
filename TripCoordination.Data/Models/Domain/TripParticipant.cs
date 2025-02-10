using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Data.Models.Domain
{
    public class TripParticipant
    {
        [Key]
        public int TripParticipantID { get; set; }
        public int TripID { get; set; }
        public int UserID { get; set; }
        public int DestinationTownID { get; set; }
        public int SeatNumber { get; set; }
        public string PickUpPoint { get; set; }

    }
}
