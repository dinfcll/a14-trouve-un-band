using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace TrouveUnBand.Models
{
    public partial class User
    {   
        public User()
        {
            this.Musicians = new HashSet<Musician>();
        }

        public int UserId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^([a-zäáàëéèíìöóòúùñçA-ZÄÀËÈÉÌÔÒÙÇ]){2,}$", ErrorMessage = "Must be at least 2 characters long, letters only")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^([a-zäáàëéèíìöóòúùñçA-ZÄÀËÈÉÌÔÒÙÇ]){2,}$", ErrorMessage = "Must be at least 2 characters long, letters only")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public DateTime BirthDate { get; set; }

       // [Key]
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^([0-9a-zäáàëéèíìöóòúùñçA-ZÄÀËÈÉÌÔÒÙÇ]){3,}$", ErrorMessage = "Must be at least 3 characters long")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Must be a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^[a-zäáàëéèíìöóòúùñçA-ZÄÀËÈÉÌÔÒÙÇ ]{2,}$", ErrorMessage = "Must be at least 2 letters long")]
        public string Location { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^[\S]{4,138}$", ErrorMessage = "Must be at least 4 letters long")]
        public string Password { get; set; }

        [NotMapped]
        [RegularExpression(@"^[\S]{4,138}$", ErrorMessage = "Must be at least 4 letters long")]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        public string PhotoName { get; set; }

        public byte[] Photo { get; set; }

        public string Gender { get; set; }

        public virtual ICollection<Musician> Musicians { get; set; }
    }
}

