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
        public DbSet<CreateGroupModel> Group { get; set; }
    }

    public class CreateGroupModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string GroupName { get; set; }
        [Required]
        public int MyStyleId { get; set; }
        [Required]
        public int MyMemberId { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string Experience { get; set; }
    }
}