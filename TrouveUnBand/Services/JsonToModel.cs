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
            var js = new JavaScriptSerializer();
            dynamic jsBand = js.Deserialize<dynamic>(stringJson);

            var myBand = new Band();
                
                myBand.Name = jsBand["info"]["Name"],
                /*myBand.Location = jsBand["info"]["Location"],
                myBand.Description = jsBand["info"]["Description"],
                Genres = GenreDao.GetGenresByNames(jsBand["Genres"]),
                Users = UserDao.GetUsersById((jsBand["BandMembers"]))*/
                
            return myBand;
        }
    }
}