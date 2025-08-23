using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Data.Models.Domain
{
    public class Town
    {
        [Key]
        public int TownID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Region { get; set; }
        public string? Country { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
