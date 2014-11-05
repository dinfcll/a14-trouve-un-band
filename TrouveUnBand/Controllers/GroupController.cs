using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Models;

namespace TrouveUnBand.Controllers
{
    public class GroupController : Controller
    {
        private TrouveUnBandEntities db = new TrouveUnBandEntities();

        public ActionResult Index()
        {
            List<Band> myBands = new List<Band>();
            if (Request.IsAuthenticated)
            {
                if (CurrentUserIsMusician(GetCurrentUser()))
                {
                    var CurrentMusician = GetCurrentMusician();
                    myBands = CurrentMusician.Bands.ToList();
                }
            }
                return View(myBands);
        }

        public ActionResult Details(int id = 0)
        {
            var band = db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        //
        // GET: /Group/Create
        [HttpGet]
        public ActionResult Create()
        {
            string MessageAlert = "";

            if (!Request.IsAuthenticated)
            {
                MessageAlert = "Vous devez être connecté pour créer un band";
                RedirectToAction("Index", "Home");
            }

            var CurrentUser = GetCurrentUser();
            var CurrentMusician = GetCurrentMusician();

            if (!CurrentUserIsMusician(CurrentUser))
            {
                MessageAlert = "Aucun profile musicien n'est associé à ce compte. Veuillez vous créer un profile musicien";
                RedirectToAction("Index", "Home");
            }

            if (Session["myBand"] == null || Session["myMusicians"] == null)
            {
                Band myBand = new Band();
                myBand.Name = "";
                myBand.Location = "";
                myBand.Description = "";
                List<Musician> myMusicians = new List<Musician>();
                myMusicians.Add(CurrentMusician);
                /* 
                    Il faut utilisé une list de musiciens à part du band car l'exécution différée de LINQ entre en conflit. 
                    Lors d'une boucle foreach, l'objet est disposé apres la première lecture, donc n'a plus d'instance a la 
                    deuxième itération de la boucle
                */
                Session["myMusicians"] = myMusicians;
                Session["myBand"] = myBand;
                // À la création de la vue le premier musicien est toujours le musicien associé au compte authentifié.
                ViewBag.CurrentMusician = CurrentMusician;
            }

            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            TempData["TempDataError"] = MessageAlert;
            return View();
        }

        //
        // POST: /Group/Create

        [HttpGet]
        public PartialViewResult SubmitInfo(Band band)
        {
            string WC = "";
            var ExistingBand = db.Bands.FirstOrDefault(x => x.Name == band.Name);

            var BandToUpdate = (Band)Session["myBand"];
            BandToUpdate.Name = band.Name;
            BandToUpdate.Location = band.Location;
            BandToUpdate.Description = band.Description;
            Session["myBand"] = BandToUpdate;

            TempData["warning"] = WC;
            return PartialView("_ConfirmCreateDialog", band);
        }
        
        [HttpGet]
        public ActionResult ConfirmCreate()
        {
            var band = (Band)Session["myBand"];
            var ExistingBand = db.Bands.FirstOrDefault(x => x.Name == band.Name);
            var CurrentUser = GetCurrentUser();
            var CurrentMusician = GetCurrentMusician();
            CurrentUserIsMusician(CurrentUser);
            if (ExistingBand != null)
            {
                    band.Name = band.Name + " (" + band.Location + ")";
                    ExistingBand.Name = ExistingBand.Name + " (" + ExistingBand.Location + ")";
                    TempData["warning"] = "Le Band existe déja. Votre band a été renommé par: " + band.Name;
            }
            try
            {
                band.Musicians = (List<Musician>)Session["myMusicians"];
                band.Musicians = null;
                CurrentMusician.Bands.Add(band);
                db.Database.Connection.Open();
                db.Bands.Add(band);
                db.SaveChanges();
                db.Database.Connection.Close();
                TempData["Success"] = "Vous avez créé un band!";
                Session["myBand"] = new Band();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["TempDataError"] = "Une erreur interne s'est produite, Réessayez plus tard " + ex.Message;
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult UpdateModal()
        {
            var myBand = (Band)Session["myBand"];
            myBand.Musicians = (List<Musician>)Session["myMusicians"];
            if (IsValidBand(myBand))
            {
                TempData["TempDataError"] = "Vous n'avez pas entré toutes les informations";
                return View("Create");
            }
            return PartialView("_ConfirmCreateDialog", myBand);
        }


        public ActionResult Edit(int id = 0)
        {
            var band = db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        [HttpPost]
        public ActionResult Edit(Band band)
        {
            if (ModelState.IsValid)
            {
                db.Entry(band).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(band);
        }

        public ActionResult Delete(int id = 0)
        {
            var band = db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var band = db.Bands.Find(id);
            db.Bands.Remove(band);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private bool CurrentUserIsMusician(User CurrentUser)
        {
            if (GetCurrentMusician() == null)
            {
                return false;
            }
            return false;
        }

        public User GetCurrentUser()
        {
            db.Database.Connection.Open();
            string Username = User.Identity.Name;
            var iQUser = db.Users.Where(x => x.Nickname == Username);
            User CurrentUser = (iQUser.ToList())[0];
            db.Database.Connection.Close();
            return CurrentUser;
            
        }

        public Musician GetCurrentMusician()
        {
            Musician CurrentMusician = GetCurrentUser().Musicians.FirstOrDefault();
            return CurrentMusician;
        }

        public bool IsValidBand(Band myBand)
        {
            if (// Steven Seagel understands and approves this lenghty condition
                String.IsNullOrEmpty(myBand.Name)
                || String.IsNullOrEmpty(myBand.Location)
                || String.IsNullOrEmpty(myBand.Description)
                || !myBand.Genres.Any() 
                || myBand.Genres == null
                || !myBand.Musicians.Any() 
                || myBand.Musicians == null
            )
            {
                return true;
            }
            return false;
        }

        [HttpPut]
        public ActionResult AddMusician(int MusicianId)
        {
            db.Database.Connection.Open();
            if (((List<Musician>)Session["myMusicians"]).Any(x => x.MusicianId == MusicianId))
            {
                TempData["TempDataError"] = "Vous avez déjà sélectionné ce musicien";
            }
            else
            {
                var Query = db.Musicians.FirstOrDefault(x => x.MusicianId == MusicianId);
                ((List<Musician>)Session["myMusicians"]).Add(Query);
            }
            db.Database.Connection.Close();
            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            return PartialView("_MusicianTab");
        }

        [HttpDelete]
        public ActionResult RemoveMusician(int Musicianid)
        {
            db.Database.Connection.Open();
            var Query = db.Musicians.FirstOrDefault(x => x.MusicianId == Musicianid);
            var myMusician = (List<Musician>)Session["myMusicians"];
            myMusician.Remove(myMusician.Single(s => s.MusicianId == Query.MusicianId));
            Session["myMusicians"] = myMusician;
            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            db.Database.Connection.Close();
            return PartialView("_MusicianTab");
        }

        [HttpPut]
        public ActionResult AddGenre(int Genrelist)
        {
            db.Database.Connection.Open();
            string RC = "";
            if (((Band)Session["myBand"]).Genres.Any(x => x.GenreId == Genrelist))
            {
                TempData["TempDataError"] = "Vous avez déja sélectionné ce genre";
            }
            else
            {
                var Query = db.Genres.FirstOrDefault(x => x.GenreId == Genrelist);
                ((Band)Session["myBand"]).Genres.Add(Query);
            }
            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            db.Database.Connection.Close();
            return PartialView("_GenreTab");
        }

        [HttpDelete]
        public ActionResult RemoveGenre(int GenreId)
        {
            db.Database.Connection.Open();
            var Query = db.Genres.FirstOrDefault(x => x.GenreId == GenreId);
            var myBand = ((Band)Session["myBand"]);
            myBand.Genres.Remove(myBand.Genres.Single( s => s.GenreId == Query.GenreId));
            Session["myBand"] = myBand;
            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            db.Database.Connection.Close();
            return PartialView("_GenreTab");
        }

        [HttpGet]
        public ActionResult SearchMusician(string SearchString)
        {
            db.Database.Connection.Open();
            string RC = "";
            List<Musician> musicians = new List<Musician>();
            if (String.IsNullOrEmpty(SearchString))
            {
                TempData["TempDataError"] = "Le champ de recherche est vide";
            }
            else
            {
                if (!String.IsNullOrEmpty(SearchString))
                {
                    musicians = (db.Musicians.Where(musician => musician.User.FirstName.Contains(SearchString) ||
                                                musician.User.LastName.Contains(SearchString) ||
                                                musician.User.Nickname.Contains(SearchString))).ToList();
                }
            }

            ViewData["SearchMusicians"] = musicians;
            db.Database.Connection.Close();
            return PartialView("_MusicianTab");
        }
    }
}
