using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripCoordination.Common.ViewModel
{
    public class CreateRouteViewModel
    {
        public int? RouteID { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "From location is required.")]
        [StringLength(100, ErrorMessage = "From location cannot exceed 100 characters.")]
        public string FromLocation { get; set; } = string.Empty;

        [Required(ErrorMessage = "To location is required.")]
        [StringLength(100, ErrorMessage = "To location cannot exceed 100 characters.")]
        public string ToLocation { get; set; } = string.Empty;
    }
}
