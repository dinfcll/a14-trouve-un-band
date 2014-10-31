using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Models
{
    public class UserDao
    {
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

    }
}