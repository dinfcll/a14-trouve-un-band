using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Models;
using System.Collections;

namespace TrouveUnBand.Controllers
{
    public class CreateGroupController : Controller
    {
        //
        // GET: /CreateGroup/

        private TUBBDContext db = new TUBBDContext();

        public ActionResult CreateGroup(CreateGroupModel model)
        {
            
            //Boucle remplissant l'arraylist avec les style présent dans la table style

            //boucle remplissant l'arraylist membre avec les donnée présent dans la table membre
            

            return View();
        }

        public ActionResult ConfirmCreateGroup(CreateGroupModel model)
        {
            return View();
        }

    }
}
