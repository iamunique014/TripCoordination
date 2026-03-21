using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class TripRequestViewModel
    {
        [Required]
        [Display(Name = "From")]
        public string FromLocation { get; set; }

        [Required]
        [Display(Name = "To")]
        public string ToLocation { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Preferred Date")]
        public DateTime DesiredDate { get; set; }

        [Display(Name = "Additional Notes")]
        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}
