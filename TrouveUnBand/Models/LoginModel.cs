using System.ComponentModel.DataAnnotations;

namespace TrouveUnBand.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "L'identifiant est requis")]
        [Display(Name = "Identifiant ou Courriel")]
        public string Nickname { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis")]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Se souvenir de moi?")]
        public bool RememberMe { get; set; }
    }
}
