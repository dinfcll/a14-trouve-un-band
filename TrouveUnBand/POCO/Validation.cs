using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Models;

namespace TrouveUnBand.POCO
{
    public static class Validation
    {
        private static TrouveUnBandEntities db = new TrouveUnBandEntities();

        public static bool CurrentUserIsMusician(User CurrentUser)
        {
            bool b = false;
            var CurrentMusician = GetCurrentMusician(CurrentUser.Nickname);
            if (CurrentMusician == null)
                b = false;
            else
                b = true;
            return b;
        }

        public static User GetCurrentUser(string Username)
        {
            db.Database.Connection.Open();
            var iQUser = db.Users.Where(x => x.Nickname == Username);
            User CurrentUser = (iQUser.ToList())[0];
            db.Database.Connection.Close();
            return CurrentUser;

        }

        public static Musician GetCurrentMusician(string Username)
        {
            Musician CurrentMusician = GetCurrentUser(Username).Musicians.FirstOrDefault();
            return CurrentMusician;
        }

        public static bool IsValidBand(Band myBand)
        {
            bool Retour = false;

            if (// Steven Seagel understands and approves this lenghty condition
                myBand.Name == ""
                || myBand.Name == null
                || myBand.Location == ""
                || myBand.Location == null
                || myBand.Description == ""
                || myBand.Description == null
                || !myBand.Genres.Any()
                || myBand.Genres == null
                || !myBand.Musicians.Any()
                || myBand.Musicians == null
            )
            {
                Retour = true;
            }
            else
            {
                Retour = false;
            }
            return Retour;
        }
    }
}