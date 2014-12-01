using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TrouveUnBand.Models;
using TrouveUnBand.POCO;
using TrouveUnBand.Services;
using TrouveUnBand.Classes;

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
            var bandMember = GetAuthenticatedBandMember();

            ViewBag.AuthenticatedUser = new JavaScriptSerializer().Serialize(bandMember);
            ViewBag.Subgenres = subgenres;

            var band = new Band();
            band.Users.Add(user);

            return View(band);
        }

        [HttpPost]
        public ActionResult Create(string bandJSON)
        {
            var myBand = JsonToModel.ToBand(bandJSON);
            var coord = Geolocalisation.GetCoordinatesByLocation(myBand.Location);
            myBand.Latitude = coord.latitude;
            myBand.Longitude = coord.longitude;
            return View("index");
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

        private BandMemberModel GetAuthenticatedBandMember()
        {
            if (!CurrentUserIsAuthenticated())
            {
                throw new Exception("User is not authenticated");
            }
            var userName = System.Web.HttpContext.Current.User.Identity.Name;
            var Query = from bandMember in db.Users
                        where bandMember.Nickname == userName
                        select new BandMemberModel()
                        {
                            User_ID = bandMember.User_ID,
                            FirstName = bandMember.FirstName,
                            LastName = bandMember.LastName,
                            Nickname = bandMember.Nickname,
                            Location = bandMember.Location
                        };

            return Query.ToList()[0];
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
