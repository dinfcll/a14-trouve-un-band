using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using TrouveUnBand.Models;

namespace TrouveUnBand.Services
{
    public static class JsonToModel
    {
        public static Band ToBand(string stringJson)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic jsBand = js.Deserialize<dynamic>(stringJson);

            Band myBand = new Band();

            myBand.Name = jsBand["info"]["Name"];
            myBand.Location = jsBand["info"]["Location"];
            myBand.Description = jsBand["info"]["Description"];
            myBand.Genres = GenreDao.GetGenresByNames(jsBand["Genres"]);
            myBand.Genres = UserDao.GetUsersById((jsBand["Members"]));

            return myBand;
        }
    }
}
