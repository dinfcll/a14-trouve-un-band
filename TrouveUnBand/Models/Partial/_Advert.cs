using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrouveUnBand.Classes;

namespace TrouveUnBand.Models
{
    [MetadataType(typeof(AdvertMetadata))]
    public partial class Advert
    {
        [NotMapped]
        public Photo PhotoCrop { get; set; }

        public sealed class AdvertMetadata
        {
            public int Advert_ID { get; set; }
            [Required(ErrorMessage = "Le type d'annonce est requis")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Vous devez être connecter pour créer des annonces")]
            public int Creator_ID { get; set; }
            [Required(ErrorMessage = "Le genre est requis")]
            public List<Genre> Genres { get; set; }
            [Required(ErrorMessage = "Une description est requise")]
            public string Description { get; set; }
            [Required(ErrorMessage = "Le status est obligatoire")]
            public string Status { get; set; }
            public string CreationDate { get; set; }
            [Required(ErrorMessage = "La date d'expiration de la demande est requise")]
            public string ExpirationDate { get; set; }
            [Required(ErrorMessage = "L'emplacement de la demande est requis")]
            public string Location { get; set; }
            public string Photo { get; set; }
        }
    }
}