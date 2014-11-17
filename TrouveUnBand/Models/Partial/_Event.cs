﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrouveUnBand.Models.Partial
{
    public partial class Event
    {
        public int EventId { get; set; }
        [Required(ErrorMessage = "Ce champ est requis")]
        public string EventName { get; set; }
        [Required(ErrorMessage = "Ce champ est requis")]
        public string EventLocation { get; set; }
        [Required(ErrorMessage = "Ce champ est requis")]
        public string EventAddress { get; set; }
        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^[a-zäáàëéèíìöóòúùñçA-ZÄÀËÈÉÌÔÒÙÇ \-]{2,}$", ErrorMessage = "Doit avoir 2 caractères minimum, lettres seulement")]
        public string EventCity { get; set; }
        [Required(ErrorMessage = "Ce champ est requis")]
        public System.DateTime EventDate { get; set; }
        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^[0-9]{1,}$", ErrorMessage = "Doit être composé de chiffres seulement")]
        public string EventMaxAudience { get; set; }
        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^[0-9]{1,}$", ErrorMessage = "Doit être composé de chiffres seulement")]
        public float EventSalary { get; set; }
        public string EventGender { get; set; }
        [Required(ErrorMessage = "Ce champ est requis")]
        [RegularExpression(@"^[0-9]{1,}$", ErrorMessage = "Doit être composé de chiffres seulement")]
        public int EventStageSize { get; set; }
        public byte[] EventPhoto { get; set; }
        public string EventCreator { get; set; }
    }
}