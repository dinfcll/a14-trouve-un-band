using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace TrouveUnBand.Models
{
    public class TUBBDContext : DbContext
    {
        public DbSet<GroupModel> Group { get; set; }
    }

    public class GroupModel
    {
        [Required]
        public int BandID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Description { get; set; }
    }
}