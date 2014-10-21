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

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^([a-zäáàëéèíìöóòúùñçA-ZÄÀËÈÉÌÔÒÙÇ]){2,}$", ErrorMessage = "Doit avoir 2 caractères minimum, et être composé que de lettres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^([a-zäáàëéèíìöóòúùñçA-ZÄÀËÈÉÌÔÒÙÇ]){2,}$", ErrorMessage = "Doit avoir 2 caractères minimum, et être composé que de lettres")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^([0-9a-zäáàëéèíìöóòúùñçA-ZÄÀËÈÉÌÔÒÙÇ]){3,}$", ErrorMessage = "Doit avoir 3 caractères minimum")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Le courriel doit être valide")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^[a-zäáàëéèíìöóòúùñçA-ZÄÀËÈÉÌÔÒÙÇ ]{2,}$", ErrorMessage = "Doit avoir 2 caractères minimum")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^[\S]{4,138}$", ErrorMessage = "Doit avoir 4 caractères minimum")]
        public string Password { get; set; }

        [NotMapped]
        [RegularExpression(@"^[\S]{4,138}$", ErrorMessage = "Doit avoir 4 caractères minimum")]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        public string PhotoName { get; set; }

        public byte[] Photo { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        public string Gender { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public virtual ICollection<Musician> Musicians { get; set; }

        [NotMapped]
        public int PicX { get; set; }

        [NotMapped]
        public int PicY { get; set; }

        [NotMapped]
        public int PicWidth { get; set; }

        [NotMapped]
        public int PicHeight { get; set; }

    }
}
