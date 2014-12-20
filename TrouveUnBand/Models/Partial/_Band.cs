using TrouveUnBand.Classes;

namespace TrouveUnBand.Models
{
    public partial class Band
    {
        public void UpdateLocationWithAPI(string location)
        {
            var coords = Geolocalisation.GetCoordinatesByLocation(location);
            Location = coords.FormattedAddress;
            Latitude = coords.Latitude;
            Longitude = coords.Longitude;
        }

        public void UpdateLocationWithAPI()
        {
            var coords = Geolocalisation.GetCoordinatesByLocation(Location);
            Location = coords.FormattedAddress;
            Latitude = coords.Latitude;
            Longitude = coords.Longitude;
        }
    }
}
