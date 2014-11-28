using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using TrouveUnBand.Models;
using TrouveUnBand.POCO;
using TrouveUnBand.Services;

namespace TrouveUnBand.Controllers
{
    public class GroupController : Controller
    {
        private TrouveUnBandEntities db = new TrouveUnBandEntities();

        public ActionResult Index()
        {
            var myBands = new List<Band>();
            if (Request.IsAuthenticated)
            {
                User currentUser = GetAuthenticatedUser();
                if (currentUser.IsBandMember())
                {
                    myBands = currentUser.Bands.ToList();
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

        [HttpGet]
        public ActionResult Create()
        {
            var subgenres = GenreDao.GetAllSubgenresByGenres();
            var user = GetAuthenticatedUser();

            ViewBag.AuthenticatedUser = user.FirstName + " " + user.LastName;
            ViewBag.Subgenres = subgenres;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Band band, User[] bandMembers, String[] cbSelectedGenres)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View();
        }

        [HttpGet]
        public PartialViewResult SubmitInfo(Band band)
        {
            var existingBand = db.Bands.FirstOrDefault(x => x.Name == band.Name);

            var bandToUpdate = (Band)Session["myBand"];
            bandToUpdate.Name = band.Name;
            bandToUpdate.Location = band.Location;
            bandToUpdate.Description = band.Description;
            Session["myBand"] = bandToUpdate;

            return PartialView("_ConfirmCreateDialog", band);
        }
        
        [HttpGet]
        public ActionResult ConfirmCreate()
        {
            var band = (Band)Session["myBand"];
            var existingBand = db.Bands.FirstOrDefault(x => x.Name == band.Name);
            var currentUser = GetAuthenticatedUser();

            if (existingBand != null)
            {
                TempData["warning"] = AlertMessages.EXISTING_BAND(existingBand, band);
            }

            try
            {
                band.Users = (List<User>)Session["myMusicians"];
                currentUser.Bands.Add(band);
                db.Database.Connection.Open();
                db.Bands.Add(band);
                db.SaveChanges();
                db.Database.Connection.Close();

                TempData["Success"] = AlertMessages.BAND_CREATION_SUCCESS(band);
                Session["myBand"] = new Band();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["TempDataError"] = AlertMessages.INTERNAL_ERROR;
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult UpdateModal()
        {
            var myBand = (Band)Session["myBand"];
            myBand.Users = (List<User>)Session["myMusicians"];
            if (IsValidBand(myBand))
            {
                TempData["TempDataError"] = AlertMessages.EMPTY_INPUT;
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
            band.Genres.Clear();
            band.Users.Clear();
            db.SaveChanges();
            db.Bands.Remove(band);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private bool CurrentUserIsAuthenticated()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return true;
            }

            return false;
        }

        private User GetAuthenticatedUser()
        {
            if (!CurrentUserIsAuthenticated())
            {
                throw new Exception("User is not authenticated");
            }
            var userName = System.Web.HttpContext.Current.User.Identity.Name;
            var authenticatedUser = db.Users.FirstOrDefault(x => x.Nickname == userName);

            return authenticatedUser;      
        }

        public bool IsValidBand(Band myBand)
        {
            if (
                String.IsNullOrEmpty(myBand.Name)
                || String.IsNullOrEmpty(myBand.Location)
                || String.IsNullOrEmpty(myBand.Description)
                || !myBand.Genres.Any() 
                || myBand.Genres == null
                || !myBand.Users.Any() 
                || myBand.Users == null
            )
            {
                return true;
            }
            return false;
        }

        public bool ContainsMusician(List<User> myMusicians, int musicianId)
        {
            if (myMusicians.Any(x => x.User_ID == musicianId))
            {
                return false;
            }
            return true;
        }

        [HttpPut]
        public ActionResult AddMusician(int userid)
        {
            if (ViewBag.BandMembers != null)
            {
                var bandMembers = ViewBag.BandMembers;
            }
        }

        [HttpDelete]
        public ActionResult RemoveMusician(int musicianid)
        {
            db.Database.Connection.Open();
            var query = db.Users.FirstOrDefault(x => x.User_ID == musicianid);
            var myMusician = (List<User>)Session["myMusicians"];
            myMusician.Remove(myMusician.Single(s => s.User_ID == query.User_ID));
            Session["myMusicians"] = myMusician;
            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            db.Database.Connection.Close();
            return PartialView("_MusicianTab");
        }

        [HttpPut]
        public ActionResult AddGenre(int genrelist)
        {
            db.Database.Connection.Open();
            var query = db.Genres.FirstOrDefault(x => x.Genre_ID == genrelist);
            if (((Band)Session["myBand"]).Genres.Any(x => x.Genre_ID == genrelist))
            {
                TempData["TempDataError"] = AlertMessages.GENRE_ALREADY_SELECTED(query);
            }
            else
            {
                ((Band)Session["myBand"]).Genres.Add(query);
            }
            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            db.Database.Connection.Close();
            return PartialView("_GenreTab");
        }

        [HttpDelete]
        public ActionResult RemoveGenre(int genreId)
        {
            db.Database.Connection.Open();
            var query = db.Genres.FirstOrDefault(x => x.Genre_ID == genreId);
            var myBand = ((Band)Session["myBand"]);
            myBand.Genres.Remove(myBand.Genres.Single( s => s.Genre_ID == query.Genre_ID));
            Session["myBand"] = myBand;
            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            db.Database.Connection.Close();

            return PartialView("_GenreTab");
        }

        [HttpGet]
        public ActionResult SearchMusician(string searchString)
        {
            var musicians = UserDao.GetMusicians(searchString);
            ViewBag.Results = musicians;
            ViewBag.ResultsCount = musicians.Count;

            return PartialView("_SearchResults");
        }
    }
}
