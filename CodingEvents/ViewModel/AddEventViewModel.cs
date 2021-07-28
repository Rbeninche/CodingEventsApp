using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.ViewModel
{
    public class AddEventViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description too long!")]
        public string Description { get; set; }

        [EmailAddress]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "Location information is required.")]
        public string Location { get; set; }

        public bool IsTrue { get { return true; } }

        [Compare("IsTrue", ErrorMessage = "Registration is required.")]
        public bool RegistrationRequired { get; set; }

        [Range(0, 100000, ErrorMessage = "The number of attendees must be between 0 and 100,000.")]
        public int NumberOfAttendees { get; set; }

    }
}
