using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Models
{
    public class InsertUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
    }
}
