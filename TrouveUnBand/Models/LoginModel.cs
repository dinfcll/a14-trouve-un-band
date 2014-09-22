using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Nickname or Email")]
        public string Nickname { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember password?")]
        public bool RememberMe { get; set; }
    }
}
