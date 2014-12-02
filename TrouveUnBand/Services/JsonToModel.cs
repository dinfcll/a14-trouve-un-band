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

            /*  expected json result
              {
                "BandMembers":  [{"User_ID":1,"FirstName":"Vincent","LastName":"Bernier","Nickname":"gogluness","Location":"St-Adalbert"},{"User_ID":2,"FirstName":"Steven","LastName":"Seagulls","Nickname":"StevenSeagulls","Location":"USA"}],

                "Genres":["Blues","Blues rock"],

                "info":{"Name":"monband","Location":"ici","Description":"ok"}
               }
            */
            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic jsBand = js.Deserialize<dynamic>(stringJson);

            Band myBand = new Band();

            myBand.Name = jsBand["Name"];
            myBand.Location = jsBand["Location"];
            myBand.Description = jsBand["Description"];

            int[] keys;
            string[] stringValues;
            jsBand["Genres"].flatten().ToArrays(out keys, out stringValues);

            myBand.Genres = GenreDao.GetGenresByNames(stringValues);

            var intValues = new List<int>();
            foreach (var item in jsBand["BandMembers"])
            {
                intValues.Add(item["User_ID"]);
            }

            myBand.Users= UserDao.GetUsersById(intValues.ToArray());


            return myBand;
        }


    }
}