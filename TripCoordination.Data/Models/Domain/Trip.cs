using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Data.Models.Domain
{
    public class Trip
    {
        [Key]
        public int TripID { get; set; }
        public int TownID { get; set; }

        [Required]
        public string CreatorUserId { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }
        [Required]
        public int Seats { get; set; }
        [Required]
        public int IsFull { get; set; }
    }
}
