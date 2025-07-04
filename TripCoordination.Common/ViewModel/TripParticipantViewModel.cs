using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class TripParticipantViewModel
    {
        public int TripParticipantID { get; set; }
        public int TripID { get; set; }
        public string DestinationName { get; set; }
        public string UserID { get; set; }
        public string PickUpPoint { get; set; }
        public int SeatNunmber { get; set; }
        public string ParticipantName { get; set; }
        public string ParticipantSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string ParticipantEmail { get; set; }
    }
}
