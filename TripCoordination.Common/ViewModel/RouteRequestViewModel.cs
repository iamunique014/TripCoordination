using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class RouteRequestViewModel
    {
        [Required]
        [Display(Name = "From Location")]
        public string FromLocation { get; set; }

        [Required]
        [Display(Name = "To Location")]
        public string ToLocation { get; set; }

        [Display(Name = "Reason (Optional)")]
        [MaxLength(500)]
        public string? Reason { get; set; }
    }
}
