using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VhakoneWeb.Models
{
    public class PersonalDetail
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(4, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [Display(Name ="Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 9)]
        [Display(Name = "ID / Passport Number")]
        public string IdNumber { get; set; }

        [Required]
        [Display(Name = "Gender")]
        [StringLength(6, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Disable?")]
        public bool Disable { get; set; }

        
        public string UserId { get; set; }
    }
}