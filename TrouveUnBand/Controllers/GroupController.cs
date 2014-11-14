using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Models;
using TrouveUnBand.Models.Partial;
using TrouveUnBand.POCO;

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
                User currentUser = GetCurrentUser();
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
            string MessageAlert = "";

            if (!Request.IsAuthenticated)
            {
                MessageAlert = AlertMessages.NOT_CONNECTED;
                return RedirectToAction("Index", "Home");
            }

            var CurrentUser = GetCurrentUser();
 
            if (!CurrentUser.isMusician())
            {
                MessageAlert = AlertMessages.NOT_MUSICIAN;
                return RedirectToAction("Index", "Home");
            }

            if (Session["myBand"] == null || Session["myMusicians"] == null)
            {
                Band myBand = new Band();
                myBand.Name = "";
                myBand.Location = "";
                myBand.Description = "";
                List<User> myMusicians = new List<User>();
                myMusicians.Add(CurrentUser);

                Session["myMusicians"] = myMusicians;
                Session["myBand"] = myBand;

                ViewBag.CurrentMusician = CurrentUser;
            }

            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            TempData["TempDataError"] = MessageAlert;
            return View();
        }

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

            if (ExistingBand != null)
            {
                TempData["warning"] = AlertMessages.EXISTING_BAND(ExistingBand, band);
            }

            try
            {
                band.Users = (List<User>)Session["myMusicians"];
                CurrentUser.Bands.Add(band);
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
            db.Bands.Remove(band);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public User GetCurrentUser()
        {
            string Username = User.Identity.Name;
            var CurrentUser = db.Users.FirstOrDefault(x => x.Nickname == Username);

            return CurrentUser;
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

        public bool ContainsMusician(List<User> myMusicians, int MusicianId)
        {
            if (myMusicians.Any(x => x.User_ID == MusicianId))
            {
                return false;
            }
            return true;
        }

        [HttpPut]
        public ActionResult AddMusician(int MusicianId)
        {
            db.Database.Connection.Open();
            var Query = db.Users.FirstOrDefault(x => x.User_ID == MusicianId);
            if (ContainsMusician((List<User>)Session["myMusicians"], MusicianId))
            {
                TempData["TempDataError"] = AlertMessages.MUSICIAN_ALREADY_SELECTED(Query);
            }
            else
            {
                ((List<User>)Session["myMusicians"]).Add(Query);
            }
            db.Database.Connection.Close();
            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            return PartialView("_MusicianTab");
        }

        [HttpDelete]
        public ActionResult RemoveMusician(int Musicianid)
        {
            db.Database.Connection.Open();
            var Query = db.Users.FirstOrDefault(x => x.User_ID == Musicianid);
            var myMusician = (List<User>)Session["myMusicians"];
            myMusician.Remove(myMusician.Single(s => s.User_ID == Query.User_ID));
            Session["myMusicians"] = myMusician;
            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            db.Database.Connection.Close();
            return PartialView("_MusicianTab");
        }

        [HttpPut]
        public ActionResult AddGenre(int Genrelist)
        {
            db.Database.Connection.Open();
            var Query = db.Genres.FirstOrDefault(x => x.Genre_ID == Genrelist);
            if (((Band)Session["myBand"]).Genres.Any(x => x.Genre_ID == Genrelist))
            {
                TempData["TempDataError"] = AlertMessages.GENRE_ALREADY_SELECTED(Query);
            }
            else
            {
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
            var Query = db.Genres.FirstOrDefault(x => x.Genre_ID == GenreId);
            var myBand = ((Band)Session["myBand"]);
            myBand.Genres.Remove(myBand.Genres.Single( s => s.Genre_ID == Query.Genre_ID));
            Session["myBand"] = myBand;
            ViewBag.GenrelistDD = new List<Genre>(db.Genres);
            db.Database.Connection.Close();
            return PartialView("_GenreTab");
        }

        [HttpGet]
        public ActionResult SearchMusician(string SearchString)
        {
            db.Database.Connection.Open();
            List<User> musicians = new List<User>();
            if (String.IsNullOrEmpty(SearchString))
            {
                TempData["TempDataError"] = AlertMessages.EMPTY_SEARCH;
            }
            else
            {
                if (!String.IsNullOrEmpty(SearchString))
                {
                    musicians = (db.Users.Where(user => user.FirstName.Contains(SearchString) ||
                                                user.LastName.Contains(SearchString) ||
                                                user.Nickname.Contains(SearchString))).ToList();
                }
            }

            ViewData["SearchMusicians"] = musicians;
            db.Database.Connection.Close();
            return PartialView("_MusicianTab");
        }
    }
}
