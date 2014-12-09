using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrouveUnBand.Models;

namespace TrouveUnBand.Classes
{
    public class BandInfo
    {
        public int Band_ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public BandInfo(Band band)
        {
            this.Band_ID = band.Band_ID;
            this.Name = band.Name;
            this.Location = band.Location;
            this.Description = band.Description;
        }
    }
}