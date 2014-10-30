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
            if (Request.IsAuthenticated)
            {
                List<Musician> CurrentMusician = new List<Musician>();
                bool b = false;
                b = CurrentUserIsMusician(GetCurrentUser(), out CurrentMusician);

                if (b)
                {
                    return View(CurrentMusician[0].Bands);
                }

            }
                return View(new List<Band>());
        }

        public ActionResult Details(int id = 0)
        {
            Band band = db.Bands.Find(id);
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
            string RC = "";
            if (Request.IsAuthenticated)
            {
                    User CurrentUser = GetCurrentUser();
                    List<Musician> CurrentMusician = new List<Musician>();
                    ViewBag.CurrentUser = CurrentUser;
                    bool b = CurrentUserIsMusician(CurrentUser, out CurrentMusician);
                    if (b)
                    {
                        if (Session["myBand"] == null || Session["myMusicians"] == null)
                        {
                            Band myBand = new Band();
                            myBand.Name = "";
                            myBand.Location = "";
                            myBand.Description = "";
                            List<Musician> myMusicians = new List<Musician>();
                            myMusicians.Add(CurrentMusician[0]);
                            /* 
                                Il faut utilisé une list de musiciens à part du band car l'exécution différée de LINQ entre en conflit. 
                                Lors d'une boucle foreach, l'objet est disposé apres la première lecture, donc n'a plus d'instance a la 
                                deuxième itération de la boucle
                            */
                            Session["myMusicians"] = myMusicians;
                            Session["myBand"] = myBand;
                            // À la création de la vue le premier musicien est toujours le musicien associé au compte authentifié.
                            ViewBag.CurrentMusician = CurrentMusician[0];
                        }
                        ViewBag.GenrelistDD = new List<Genre>(db.Genres);
                        return View("Create");
                    }
                    else
                    {
                        RC = "Aucun profile musicien n'est associé à ce compte. Veuillez vous créer un profile musicien";
                        RedirectToAction("Index", "Home");
                    }
            }
            else
            {
                RC = "Vous devez être connecté pour créer un band";
                RedirectToAction("Index", "Home");
            }
            TempData["TempDataError"] = RC;
            return View();
        }

        //
        // POST: /Group/Create

        [HttpPost]
        public PartialViewResult SubmitInfo(Band band)
        {
            string WC = "";
            Band ExistingBand = db.Bands.FirstOrDefault(x => x.Name == band.Name);
            if (ExistingBand != null)
            {
                if (!ExistingBand.Musicians.Any(x => x.MusicianId == ((GetCurrentUser()).Musicians.ToList()[0]).MusicianId))
                {
                    WC = "Le Band existe déja. Votre band sera renommé par: " + band.Name;
                }
            }

            Band BandToUpdate = (Band)Session["myBand"];
            BandToUpdate.Name = band.Name;
            BandToUpdate.Location = band.Location;
            BandToUpdate.Description = band.Description;
            Session["myBand"] = BandToUpdate;

            TempData["warning"] = WC;
            return PartialView("_basetab", band);
        }
        
        [HttpPost]
        public ActionResult ConfirmCreate()
        {
            string WC = "";
            string RC = "";

            Band band = (Band)Session["myBand"];
            Band ExistingBand = db.Bands.FirstOrDefault(x => x.Name == band.Name);
            if (ExistingBand != null)
            {
                if (!ExistingBand.Musicians.Any(x => x.MusicianId == ((GetCurrentUser()).Musicians.ToList()[0]).MusicianId))
                {
                    band.Name = band.Name + " (" + band.Location + ")";
                    ExistingBand.Name = ExistingBand.Name + " (" + ExistingBand.Location + ")";
                    WC = "Le Band existe déja. Votre band a été renommé par: " + band.Name;
                }
            }
            try
            {
                band.Musicians = (List<Musician>)Session["myMusicians"];
                db.Bands.Add(band);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                RC = "Une erreur interne s'est produite, Réessayez plus tard " + ex.Message;
                Console.WriteLine(ex.Message);
            }

            TempData["warning"] = WC;
            TempData["TempDataError"] = RC;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UpdateModal()
        {
            Band myBand = (Band)Session["myBand"];
            myBand.Musicians = (List<Musician>)Session["myMusicians"];
            return PartialView("_ConfirmCreateDialog", myBand);
        }

        public ActionResult Edit(int id = 0)
        {
            Band band = db.Bands.Find(id);
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
            Band band = db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Band band = db.Bands.Find(id);
            db.Bands.Remove(band);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private bool CurrentUserIsMusician(User CurrentUser, out List<Musician> CurrentMusician)
        {
            bool b = false;
            CurrentMusician = CurrentUser.Musicians.ToList();
            if (CurrentMusician == null)
                b = false;
            else
                b = true;
            return b;
        }

        public User GetCurrentUser()
        {
            string Username = User.Identity.Name;
            var iQUser = db.Users.Where(x => x.Nickname == Username);
            User CurrentUser = (iQUser.ToList())[0];
            return CurrentUser;
        }

        [HttpPut]
        public ActionResult AddMusician(int MusicianId)
        {
            string RC = "";
            if (((List<Musician>)Session["myMusicians"]).Any(x => x.MusicianId == MusicianId))
            {
                RC = "Vous avez déja sélectionner ce musiciens";
            }
            else
            {
                var Query = db.Musicians.FirstOrDefault(x => x.MusicianId == MusicianId);
                ((List<Musician>)Session["myMusicians"]).Add(Query);
            }
            TempData["TempDataError"] = RC;
            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            return PartialView("_MusicianTab");
        }

        [HttpDelete]
        public ActionResult RemoveMusician(int Musicianid)
        {
            Musician Query = db.Musicians.FirstOrDefault(x => x.MusicianId == Musicianid);
            List<Musician> myMusician = (List<Musician>)Session["myMusicians"];
            myMusician.Remove(myMusician.Single(s => s.MusicianId == Query.MusicianId));
            Session["myMusicians"] = myMusician;
            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            return PartialView("_MusicianTab");
        }

        [HttpPut]
        public ActionResult AddGenre(int Genrelist)
        {
            string RC = "";
            if (((Band)Session["myBand"]).Genres.Any(x => x.GenreId == Genrelist))
            {
                RC = "Vous avez déja sélectionner ce genre";
            }
            else
            {
                Genre Query = db.Genres.FirstOrDefault(x => x.GenreId == Genrelist);
                ((Band)Session["myBand"]).Genres.Add(Query);
            }
            TempData["TempDataError"] = RC;
            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            return PartialView("_GenreTab");
        }

        [HttpDelete]
        public ActionResult RemoveGenre(int GenreId)
        {
            Genre Query = db.Genres.FirstOrDefault(x => x.GenreId == GenreId);
            Band myBand = ((Band)Session["myBand"]);
            myBand.Genres.Remove(myBand.Genres.Single( s => s.GenreId == Query.GenreId));
            Session["myBand"] = myBand;
            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            return PartialView("_GenreTab");
        }

        [HttpGet]
        public ActionResult SearchMusician(string SearchString)
        {
            string RC = "";
            List<Musician> musicians = new List<Musician>();
            if (String.IsNullOrEmpty(SearchString))
            {
                RC = "Le champ de recherche est vide";
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
            TempData["TempDataError"] = RC;
            return PartialView("_MusicianTab");
        }

        public ActionResult BaseSubmit(Band myBand)
        {
            return View();
        }
    }
}

