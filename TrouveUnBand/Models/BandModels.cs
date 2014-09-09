using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Models
{
    public class BandModels
    {
        [Key]
        public int IDBand { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
    }

    public class TUBDBContext : DbContext
    {
        public DbSet<BandModels> Band { get; set; }
    }
}