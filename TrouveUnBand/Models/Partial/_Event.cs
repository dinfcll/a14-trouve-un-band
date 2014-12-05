using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrouveUnBand.Classes;

namespace TrouveUnBand.Models
{
    [MetadataType(typeof(Event.EventMetadata))]
    public partial class Event
    {
        [NotMapped]
        public Photo PhotoCrop { get; set; }

        public sealed class EventMetadata
        {
            public int Event_ID { get; set; }
            [Required(ErrorMessage = "Ce champ est requis")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Ce champ est requis")]
            public string Location { get; set; }
            [Required(ErrorMessage = "Ce champ est requis")]
            public string Address { get; set; }
            [Required(ErrorMessage = "Ce champ est requis")]
            [RegularExpression(@"^[a-zäáàâëéèêíìöóòôúùñçA-ZÂÄÀÊËÈÉÌÔÔÒÙÇ \-]{2,}$", ErrorMessage = "Doit avoir 2 caractères minimum, lettres seulement")]
            public string City { get; set; }
            [Required(ErrorMessage = "Ce champ est requis")]
            public System.DateTime EventDate { get; set; }
            [Required(ErrorMessage = "Ce champ est requis")]
            [RegularExpression(@"^[0-9]{1,}$", ErrorMessage = "Doit être composé de chiffres seulement")]
            public string MaxAudience { get; set; }
            [Required(ErrorMessage = "Ce champ est requis")]
            [RegularExpression(@"^[0-9]{1,}$", ErrorMessage = "Doit être composé de chiffres seulement")]
            public float Salary { get; set; }
            public List<Genre> Genres { get; set; }
            [Required(ErrorMessage = "Ce champ est requis")]
            //[RegularExpression(@"^[0-9]{1,}$", ErrorMessage = "Doit être composé de chiffres seulement")]
            public string StageSize { get; set; }
            public string Photo { get; set; }
            public string Creator_ID { get; set; }
        }
    }
}
