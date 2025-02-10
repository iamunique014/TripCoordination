using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Data.Models.Domain
{
    public class Residence
    {
        [Key]
        public int ResidenceID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
