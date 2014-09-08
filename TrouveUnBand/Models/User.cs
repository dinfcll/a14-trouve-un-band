using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrouveUnBand.Models
{
    [Table("Users")]
    public class User
    {
        public int IDUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class TBDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}