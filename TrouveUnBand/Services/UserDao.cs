using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrouveUnBand.Classes;

namespace TrouveUnBand.Models
{
    public class UserDao
    {
        public static List<User> GetUsersByDistance(double latitude, double longitude, double radius)
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();

            var users = from user in db.Users select user;
            foreach (var user in users)
            {
                var distance = Geolocalisation.GetDistance(latitude, longitude, user.Latitude, user.Longitude);
            }

            return users.ToList();
        }

        public static List<User> GetUsers(string userName)
        {
            var results = GetUsers(userName, "");

            return results;
        }

        public static List<User> GetUsers(string userName, string location)
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();
            List<User> lstResults = new List<User>();

            var users = from user in db.Users
                        select user;

            if (!String.IsNullOrEmpty(userName))
            {
                users = users.Where(user => user.FirstName.Contains(userName) ||
                user.LastName.Contains(userName) ||
                user.Nickname.Contains(userName));
            }

            if (!String.IsNullOrEmpty(location))
            {
                users = users.Where(user => user.Location.Contains(location));
            }

            lstResults.AddRange(users);

            return lstResults;
        }

        public static List<User> GetAllUsers()
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();
            List<User> eventList = new List<User>();
            var user = db.Users;
            eventList.AddRange(user);

            return eventList;
        }

        public static List<User> GetMusicians(string[] genres, string userName, string location, int radius)
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();
            List<User> users = new List<User>();

            users = db.Users.Where(x => x.FirstName.Contains(userName) || 
                                   x.LastName.Contains(userName) || 
                                   x.Nickname.Contains(userName)).ToList();

            foreach (var user in users)
            {
                if (!user.isMusician())
                {
                    users.Remove(user);
                }

                foreach (var genre in genres)
                {
                    if (!user.Genres.Any(x => x.Name == genre))
                    {
                        users.Remove(user);
                    }
                }
            }

            if (!String.IsNullOrEmpty(location))
            {
                var usersToRemove = new List<User>();
                var coordinates = Geolocalisation.GetCoordinatesByLocation(location);
                foreach (var user in users)
                {
                    var distance = Geolocalisation.GetDistance(user.Latitude, user.Longitude, coordinates.latitude, coordinates.longitude);
                    if (distance > radius)
                        usersToRemove.Add(user);
                }

                foreach (var user in usersToRemove)
                {
                    users.Remove(user);
                }
            }

            return users;
        }

        public static List<User> GetMusicians(int? genreID, string userName, string location, int radius)
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();
            var users = db.Users.ToList();

            if (!String.IsNullOrEmpty(userName))
            {
                users = users.Where(user => user.FirstName.Contains(userName) ||
                                   user.LastName.Contains(userName) ||
                                   user.Nickname.Contains(userName)).ToList();
            }

            if (!String.IsNullOrEmpty(location))
            {
                var usersToRemove = new List<User>();
                var coordinates = Geolocalisation.GetCoordinatesByLocation(location);
                foreach (var user in users)
                {
                    var distance = Geolocalisation.GetDistance(user.Latitude, user.Longitude, coordinates.latitude, coordinates.longitude);
                    if (distance > radius)
                        usersToRemove.Add(user);
                }

                foreach (var user in usersToRemove)
                {
                    users.Remove(user);
                }
            }

            if (genreID != null)
            {
                users = users.Where(user => user.Genres.Any(genre => genre.Genre_ID == genreID)).ToList();
            }

            return users;
        }

        public static List<User> GetMusicians(string userName)
        {
            TrouveUnBandEntities db = new TrouveUnBandEntities();
            var users = db.Users.ToList();

            if (!String.IsNullOrEmpty(userName))
            {
                foreach (var user in users)
                {
                    if (!user.isMusician())
                    {
                        users.Remove(user);
                    }
                }

                users = users.Where(user => user.FirstName.Contains(userName) ||
                                   user.LastName.Contains(userName) ||
                                   user.Nickname.Contains(userName)).ToList();
            }

            return users;
        }
    }
}
