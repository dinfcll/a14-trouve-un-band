using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace TrouveUnBand.Models
{
    public class User
    {
        [Required(ErrorMessage="This field is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@".*@.*", ErrorMessage = "Must be a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string ConfirmPassword { get; set; }
    }
}