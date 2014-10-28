using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrouveUnBand.Models
{
    public class BandCreation
    {
        public BandCreation()
        {
            this.Genres = new HashSet<Genre>();
            this.Musicians = new HashSet<Musician>();
        }

        [Required(ErrorMessage = "Le nom est requis")]
        [StringLength(50, ErrorMessage = "Le {0} ne doit pas excéder {1} characters de long")]
        [Display(Name = "Nom du band")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La description est requise")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "La location est requise")]
        [StringLength(100, ErrorMessage = "La {0} ne doit pas excéder {1} characters de long")]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Au moins un genre est requis")]
        [Display(Name = "Genre(s)")]
        public virtual ICollection<Genre> Genres { get; set; }

        [Required(ErrorMessage = "Au moins un musicien est requis")]
        [Display(Name = "Musiciens(s)")]
        public virtual ICollection<Musician> Musicians { get; set; }
    }
}