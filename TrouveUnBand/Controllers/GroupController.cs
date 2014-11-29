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

            var band = new Band();
            band.Users.Add(user);

            return View(band);
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
        public ActionResult ConfirmCreate(Band model)
        {
            var queryExistingBand = from Q in db.Bands
                                    where Q.Name == model.Name
                                    select Q;

            if (queryExistingBand.Any())
            {
                var existingBand = queryExistingBand.ToList()[0];
                if (existingBand != null)
                {
                    TempData["warning"] = AlertMessages.EXISTING_BAND(existingBand, model);
                    db.Entry(existingBand).State = EntityState.Unchanged;
                }
            }

            try
            {
                db.Database.Connection.Open();
                db.Bands.Add(model);
                db.SaveChanges();
                db.Database.Connection.Close();

                TempData["Success"] = AlertMessages.BAND_CREATION_SUCCESS(model);
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
        public ActionResult UpdateModal(Band model)
        {
            if (IsValidBand(model))
            {
                TempData["TempDataError"] = AlertMessages.EMPTY_INPUT;
                return View("Create");
            }

            return PartialView("_ConfirmCreateDialog", model);
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

        [HttpGet]
        public ActionResult SearchMusician(string searchString)
        {
            var musicians = UserDao.SearchBandMembers(searchString);
            ViewBag.Results = musicians;
            ViewBag.ResultsCount = musicians.Count;

            return PartialView("_MusicianFinder");
        }
    }
}
