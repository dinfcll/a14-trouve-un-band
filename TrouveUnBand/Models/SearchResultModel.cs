using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrouveUnBand.Models
{
    //Class used to structure information returned by the search function.
    public class SearchResultModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}