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
            Band_ID = band.Band_ID;
            Name = band.Name;
            Location = band.Location;
            Description = band.Description;
        }
    }
}
