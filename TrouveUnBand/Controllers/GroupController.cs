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
                Musician CurrentMusician = new Musician();
                ViewBag.CurrentUser = CurrentUser;
                if (CurrentUserIsMusician(CurrentUser, out CurrentMusician))
                {
                    ViewData["Musician"] = CurrentMusician;
                    ViewBag.CurrentMusician = CurrentMusician;
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

        public PartialViewResult SearchUser(String Username)
        {
            return null;
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

        private bool CurrentUserIsMusician(User CurrentUser, out Musician CurrentMusician)
        {
            bool b = false;
            CurrentMusician = CurrentUser.Musicians.FirstOrDefault();
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
            User CurrentUser = iQUser.FirstOrDefault();
            return CurrentUser;
        }

        public void AddMusician(Band band, Musician musician)
        {
            
        }

        public void AddGenre(Band band, Genre genre)
        {

        }

        public void RemoveMusician(Band band, Musician musician)
        {

        }

        public void RemoveGenre(Band band, Genre genre)
        {

        }
    }
}

