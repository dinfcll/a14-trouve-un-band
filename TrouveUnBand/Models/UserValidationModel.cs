using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrouveUnBand.Classes;

namespace TrouveUnBand.Models
{
    public class UserValidation : User
    {
        public UserValidation()
        {
            this.Musicians = new HashSet<Musician>();
            ProfilePicture = new Photo();
        }

        public UserValidation(User user)
        {
            ProfilePicture = new Photo();
            UserId = user.UserId;
            FirstName = user.FirstName;
            BirthDate = user.BirthDate;
            Email = user.Email;
            Gender = user.Gender;
            LastName = user.LastName;
            Latitude = (double)user.Latitude;
            Location = user.Location;
            Longitude = (double)user.Longitude;
            Musicians = user.Musicians;
            Nickname = user.Nickname;
            Password = user.Password;
            ProfilePicture.PhotoArray = user.Photo;
            if (user.Photo != null)
            {
                ProfilePicture.PhotoSrc = "data:image/jpeg;base64," + Convert.ToBase64String(user.Photo);
            }
            UserId = user.UserId;
        }

        new public int UserId { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^([a-zäáàëéèíìöóòúùñçA-ZÄÀËÈÉÌÔÒÙÇ]){2,}$", ErrorMessage = "Doit avoir 2 caractères minimum, et être composé que de lettres")]
        new public string FirstName { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^([a-zäáàëéèíìöóòúùñçA-ZÄÀËÈÉÌÔÒÙÇ]){2,}$", ErrorMessage = "Doit avoir 2 caractères minimum, et être composé que de lettres")]
        new public string LastName { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        new public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^([0-9a-zäáàëéèíìöóòúùñçA-ZÄÀËÈÉÌÔÒÙÇ]){3,}$", ErrorMessage = "Doit avoir 3 caractères minimum")]
        new public string Nickname { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Le courriel doit être valide")]
        new public string Email { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^[a-zäáàëéèíìöóòúùñçA-ZÄÀËÈÉÌÔÒÙÇ \-]{2,}$", ErrorMessage = "Doit avoir 2 caractères minimum, lettres seulement")]
        new public string Location { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^[\S]{4,138}$", ErrorMessage = "Doit avoir 4 caractères minimum")]
        new public string Password { get; set; }

        [NotMapped]
        [RegularExpression(@"^[\S]{4,138}$", ErrorMessage = "Doit avoir 4 caractères minimum")]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        public Photo ProfilePicture { get; set; }

        [Required(ErrorMessage = "Ce champ est requis")]
        new public string Gender { get; set; }

        new public double Latitude { get; set; }

        new public double Longitude { get; set; }
    }
}
