using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrouveUnBand.Classes;

namespace TrouveUnBand.Models
{
    public partial class User
    {
        public User()
        {
            this.Musicians = new HashSet<Musician>();
            this.Adverts = new HashSet<Advert>();
            ProfilePicture = new Photo();
        }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^([a-z���������������A-Z����������]){2,}$", ErrorMessage = "Doit avoir 2 caract�res minimum, et �tre compos� que de lettres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^([a-z���������������A-Z����������]){2,}$", ErrorMessage = "Doit avoir 2 caract�res minimum, et �tre compos� que de lettres")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^([0-9a-z���������������A-Z����������]){3,}$", ErrorMessage = "Doit avoir 3 caract�res minimum")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Le courriel doit �tre valide")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^[a-z���������������A-Z���������� \-]{2,}$", ErrorMessage = "Doit avoir 2 caract�res minimum, lettres seulement")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^[\S]{4,138}$", ErrorMessage = "Doit avoir 4 caract�res minimum")]
        public string Password { get; set; }

        [NotMapped]
        [RegularExpression(@"^[\S]{4,138}$", ErrorMessage = "Doit avoir 4 caract�res minimum")]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        public Photo ProfilePicture { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        public string Gender { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public byte[] Photo { get; set; }

        public virtual ICollection<Musician> Musicians { get; set; }
        public virtual ICollection<Advert> Adverts { get; set; }
    }

}