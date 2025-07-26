using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TripCoordination.Common.ViewModel
{
    public class CreateProfileViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }

        [RegularExpression(@"^(?:\+27|0)[6-8][0-9]{8}$", ErrorMessage = "Please enter a valid South African phone number.")]
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ResidenceID { get; set; }
        public string? UserID { get; set; }


        [ValidateNever]
        public IEnumerable<SelectListItem>? AvailableResidences { get; set; }
    }
}
