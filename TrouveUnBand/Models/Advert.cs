//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrouveUnBand.Models
{
    public partial class Advert
    {
        public int AdvertId { get; set; }

        [Required(ErrorMessage = "Le type d'annonce est requis")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Vous devez �tre connecter pour cr�er des annonces")]
        public int Creator { get; set; }

        [Required(ErrorMessage = "Le genre est requis")]
        public int GenresAdvert { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Le status est obligatoire")]
        public string Status { get; set; }

        public System.DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "La date d'expiration de la demande est requis")]
        public System.DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "L'emplacement de la demande est requis")]
        public string Location { get; set; }

        public byte[] AdvertPhoto { get; set; }

    
        public virtual User User { get; set; }
        public virtual Genre Genre { get; set; }
    }
    
}
