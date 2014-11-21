using System;
using System.Collections.Generic;
using System.Linq;
using TrouveUnBand.Classes;
using TrouveUnBand.Models;

namespace TrouveUnBand.Services
{
    public class UserDao
    {
        public static List<User> GetAllUsers()
        {
            var db = new TrouveUnBandEntities();
            var eventList = new List<User>();
            var user = db.Users;
            eventList.AddRange(user);

            return eventList;
        }

        public static List<User> GetMusicians(string[] genres, string userName, string location, int radius)
        {
            var db = new TrouveUnBandEntities();
            var users = db.Users.ToList();
            var usersToRemove = new List<User>();

            
            if(!String.IsNullOrEmpty(userName))
            {
                users = users.Where(x => x.FirstName.Contains(userName) || 
                                   x.LastName.Contains(userName) || 
                                   x.Nickname.Contains(userName)).ToList();
            }

            foreach (var user in users)
            {
                if (!user.isMusician())
                {
                    usersToRemove.Add(user);
                }

                if (genres != null)
                {
                    foreach (var genre in genres)
                    {
                        if (user.Genres.All(x => x.Name != genre))
                        {
                            usersToRemove.Add(user);
                        }
                    }
                }
            }

            if (!String.IsNullOrEmpty(location))
            {
                foreach (var user in users)
                {
                    if (Geolocalisation.CheckIfInRange(user.Location, location, radius))
                    {
                        usersToRemove.Add(user);
                    }
                }
            }

            foreach (var user in usersToRemove)
            {
                users.Remove(user);
            }

            return users;
        }

        public static List<User> GetMusicians(int? genreID, string userName, string location, int radius)
        {
            var db = new TrouveUnBandEntities();
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

                foreach (var user in users)
                {
                    if (!Geolocalisation.CheckIfInRange(user.Location, location, radius))
                    {
                        usersToRemove.Add(user);
                    }
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
            var db = new TrouveUnBandEntities();
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
