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
        public string GroupName { get; set; }
        [Required]
        public int MyStyleId { get; set; }
        [Required]
        public int MyMemberId { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string Experience { get; set; }

        public static IEnumerable<Style> Styles = new List<Style> 
        { 
            new Style 
            {
                StyleId = 1,
                Name = "Rock"
            },
            new Style 
            {
                StyleId = 2,
                Name = "Pop"
            }
        };

        public static IEnumerable<Member> Members = new List<Member> 
        { 
            new Member 
            {
                MemberId = 1,
                Name = "Renaud"
            },
            new Member
            {
                MemberId = 2,
                Name = "Vince B"
            },
            new Member
            {
                MemberId = 2,
                Name = "Vince D"
            },
            new Member
            {
                MemberId = 2,
                Name = "Fred"
            },
        };
    }
}