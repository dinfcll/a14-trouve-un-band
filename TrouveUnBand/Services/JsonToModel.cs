using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Razor;
using System.Web.Script.Serialization;
using DotNetOpenAuth.Messaging;
using TrouveUnBand.Models;

namespace TrouveUnBand.Services
{
    public static class JsonToModel
    {
        public static Band ToBand(string stringJson, TrouveUnBandEntities db)
        {
            var js = new JavaScriptSerializer();
            dynamic jsBand = js.Deserialize<dynamic>(stringJson);

            var myBand = new Band();

            myBand.Name = jsBand["name"];
            myBand.Location = jsBand["location"];
            myBand.Description = jsBand["description"];

            var stringValues = new List<string>();

            foreach (var item in jsBand["genres"])
            {
                stringValues.Add(item);
            }

            myBand.Genres.AddRange(GenreDao.GetGenresByNames(stringValues.ToArray(), db));

            var intValues = new List<int>();
            foreach (var item in jsBand["members"])
            {
                intValues.Add(Convert.ToInt32(item["id"]));
            }

            myBand.Users.AddRange(UserDao.GetUsersById(intValues.ToArray(), db));

            return myBand;
        }


    }
}
