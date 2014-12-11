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
        public static Band ToBand(string stringJson)
        {
            var js = new JavaScriptSerializer();
            dynamic jsBand = js.Deserialize<dynamic>(stringJson);

            var myBand = new Band();

            myBand.Name = jsBand["Name"];
            myBand.Location = jsBand["Location"];
            myBand.Description = jsBand["Description"];

            var stringValues = new List<string>();

            foreach (var item in jsBand["Genres"])
            {
                stringValues.Add(item);
            }

            myBand.Genres = GenreDao.GetGenresByNames(stringValues.ToArray());

            var intValues = new List<int>();
            foreach (var item in jsBand["BandMembers"])
            {
                intValues.Add(item["User_ID"]);
            }

            myBand.Users= UserDao.GetUsersById(intValues.ToArray());

            myBand.Photo = jsBand["Photo"];
            return myBand;
        }


    }
}
