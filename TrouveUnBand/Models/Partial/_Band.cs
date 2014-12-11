using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TrouveUnBand.Classes;

namespace TrouveUnBand.Models
{
        [MetadataType(typeof(Band.BandMetadata))]
        public partial class Band
        {
            [NotMapped]
            public Photo PhotoCrop { get; set; }

            public sealed class BandMetadata
            {
                public string Photo { get; set; }
            }
        }
}