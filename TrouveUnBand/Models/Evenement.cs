//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrouveUnBand.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Evenement
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public string EventAddress { get; set; }
        public string EventDate { get; set; }
        public string EventMaxAudience { get; set; }
        public string EventSalary { get; set; }
        public string EventGender { get; set; }
        public Nullable<int> EventStageSize { get; set; }
    }
}