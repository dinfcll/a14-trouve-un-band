using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TrouveUnBand.Classes;

namespace TrouveUnBand.Models
{
    public partial class Band
    {
        public void UpdateLocationWithAPI(string location)
        {
            var coords = Geolocalisation.GetCoordinatesByLocation(location);
            this.Location = coords.FormattedAddress;
            this.Latitude = coords.Latitude;
            this.Longitude = coords.Longitude;
        }

        public void UpdateLocationWithAPI()
        {
            var coords = Geolocalisation.GetCoordinatesByLocation(this.Location);
            this.Location = coords.FormattedAddress;
            this.Latitude = coords.Latitude;
            this.Longitude = coords.Longitude;
        }
    }
}
