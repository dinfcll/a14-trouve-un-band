using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrouveUnBand.Models
{
    public class SearchParameters
    {
        private TrouveUnBandEntities db = new TrouveUnBandEntities();
        public SelectList categoriesDDL = new SelectList(new List<Object>{
                new { value=1, text="tout le monde" },
                new { value=2, text="des groupes" },
                new { value=3, text="des musiciens" },
                new { value=4, text="des utilisateurs" }
        }, "value", "text");
    }
}