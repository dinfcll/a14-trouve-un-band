using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrouveUnBand.Models
{
    public class GroupModel
    {
        [Required]
        string GroupName { get; set; }
        [Required]
        string[] Style { get; set; }
        [Required]
        string[] Member { get; set; }
        [Required]
        string Region { get; set; }
        [Required]
        string Experience { get; set; }

    }
}