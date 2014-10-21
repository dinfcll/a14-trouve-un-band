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
            return View(db.Bands.ToList());
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
                    ViewData["InCreationBand"] = new Band();
                    ViewData["Musicians"] = CurrentMusician;
                    //à la création de la vue le premier musicien est toujours le musicien associé au compte authentifié.
                    ViewBag.CurrentMusician = CurrentMusician[0];
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
        public PartialViewResult CreateSubmit(Band band)
        {
            string RC = "";
            string ViewNameToReturn = "";
            Band ExistingBand = db.Bands.FirstOrDefault(x => x.Name == band.Name);
            if (ExistingBand == null)
            {
                ViewNameToReturn = "_ConfirmCreate";
            }
            else
            {
                ExistingBand.Name = ExistingBand.Name + " (" + ExistingBand.Location + ")";
                db.SaveChanges();
                band.Name = band.Name + " (" + band.Location + ")";
                RC = "Le Band existe déja. Votre band a été renommé par: " + band.Name;
            }

            TempData["TempDataError"] = RC;
            return PartialView(ViewNameToReturn, band);
        }

        
        [HttpPost]
        public ActionResult ConfirmCreate(Band band)
        {
            try
            {
                db.Bands.Add(band);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index", "Home");
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

        public void AddMusician(Musician musician)
        {
            List<Musician> Musicians = (List<Musician>)ViewData["Musicians"];
            Musicians.Add(musician);
            ViewData["Musicians"] = Musicians;
        }

        public void RemoveMusician(Musician musician)
        {
            List<Musician> Musicians = (List<Musician>)ViewData["Musicians"];
            Musicians.Remove(musician);
            ViewData["Musicians"] = Musicians;
        }

        public void AddGenre(Genre genre)
        {
            List<Genre> Genres = (List<Genre>)ViewData["Genres"];
            Genres.Add(genre);
            ViewData["genre"] = Genres;
        }

        public void RemoveGenre(Genre genre)
        {
            List<Genre> Genres = (List<Genre>)ViewData["Genres"];
            Genres.Remove(genre);
            ViewData["Genres"] = Genres;
        }

        public List<Musician> SearchGenre(String SearchString)
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

            return musicians;
        }
    }
}

