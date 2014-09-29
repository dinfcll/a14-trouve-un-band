using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrouveUnBand.Models
{
    public class DBModels
    {
        public class Band
        {
            [Key]
            public int IDBand { get; set; }
            public string Name { get; set; }
            public string Style { get; set; }
            public string Description { get; set; }
            public string Location { get; set; }
        }

        public class DBTUBContext : DbContext
        {
            public DBTUBContext() : base("name=DBTUBContext") {}
            public DbSet<User> User { get; set; }
        }
    }
}

