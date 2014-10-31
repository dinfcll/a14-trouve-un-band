using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Models
{
    public class Musician_Instrument
    {
        public int MusicianId { get; set; }
        public string MusicianName { get; set; }
        public List<string> InstrumentNames { get; set; }
        public List<string> Skills { get; set; }

        public Musician_Instrument()
        {
            InstrumentNames = new List<string>();
            Skills = new List<string>();
        }
    }
}
