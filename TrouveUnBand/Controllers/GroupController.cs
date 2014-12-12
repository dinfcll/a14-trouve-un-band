using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TrouveUnBand.Models;
using TrouveUnBand.POCO;
using TrouveUnBand.Services;
using TrouveUnBand.ViewModels;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

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
            InitialiseSessionForBandMembers();

            var bandModel = new BandCreationViewModel();      
            var authUser = GetAuthenticatedUser();

            bandModel.Band.Users.Add(authUser);

            AddBandMemberToSession(new BandMemberModel
            {
                FirstName = authUser.FirstName,
                LastName = authUser.LastName,
                Location = authUser.Location,
                User_ID = authUser.User_ID
            });

            return View(bandModel);
        }

        [HttpPost]
        public ActionResult Create(BandCreationViewModel bandViewModel, string[] cbSelectedGenres)
        {
            var bandIsNotUnique = db.Bands.Any(x => x.Name == bandViewModel.Band.Name && x.Location == bandViewModel.Band.Location);

            if (bandIsNotUnique)
            {
                return View();
            }

            foreach (var genreName in cbSelectedGenres)
            {
                var genre = db.Genres.FirstOrDefault(x => x.Name == genreName);
                bandViewModel.Band.Genres.Add(genre);
            }

            foreach (var musician in (List<BandMemberModel>)Session["BandMembers"])
            {
                var user = db.Users.FirstOrDefault(x => x.User_ID == musician.User_ID);
                bandViewModel.Band.Users.Add(user);
            }

            bandViewModel.Band.UpdateLocationWithAPI();

            try
            {
                db.Database.Connection.Open();
                db.Bands.Add(bandViewModel.Band);
                db.SaveChanges();
                db.Database.Connection.Close();

                TempData["Success"] = AlertMessages.BAND_CREATION_SUCCESS(bandViewModel.Band);
            }
            catch (Exception ex)
            {
                TempData["TempDataError"] = AlertMessages.INTERNAL_ERROR;
                Console.WriteLine(ex.Message);
            }

            return Redirect("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            var bandCreationModel = new BandCreationViewModel();
            var band = db.Bands.Find(id);

            if (band == null)
            {
                return HttpNotFound();
            }

            bandCreationModel.Band = band;
            InitialiseSessionForBandMembers();

            try
            {
                foreach (var user in band.Users)
                {
                    AddBandMemberToSession(user.User_ID);
                }
            }
            catch (NullReferenceException ex)
            {
                TempData["TempDataError"] = AlertMessages.INTERNAL_ERROR;
                Console.WriteLine(ex.Message);
            }

            return View(bandCreationModel);
        }

        [HttpPost]
        public ActionResult Edit(BandCreationViewModel bandCreationModel, string[] cbSelectedGenres)
        {
            try
            {
                db.Entry(bandCreationModel.Band).State = EntityState.Modified;
                db.SaveChanges();

                ((IObjectContextAdapter) db).ObjectContext.Detach(bandCreationModel.Band);

                var bandToUpdate = db.Bands.Single(x => x.Band_ID == bandCreationModel.Band.Band_ID);
                db.Set(typeof (Band)).Attach(bandToUpdate);
                bandToUpdate.Genres.Clear();
                bandToUpdate.Users.Clear();

                foreach (var genreName in cbSelectedGenres)
                {
                    var genre = db.Genres.FirstOrDefault(x => x.Name == genreName);
                    bandToUpdate.Genres.Add(genre);
                }

                foreach (var musician in (List<BandMemberModel>) Session["BandMembers"])
                {
                    var user = db.Users.FirstOrDefault(x => x.User_ID == musician.User_ID);
                    bandToUpdate.Users.Add(user);
                }

                bandToUpdate.UpdateLocationWithAPI();

                db.Entry(bandToUpdate).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Success"] = AlertMessages.BAND_CREATION_SUCCESS(bandCreationModel.Band);
            }
            catch (NullReferenceException ex)
            {
                TempData["TempDataError"] = AlertMessages.INTERNAL_ERROR;
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                TempData["TempDataError"] = AlertMessages.INTERNAL_ERROR;
                Console.WriteLine(ex.Message);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Work around for known bug
                DbUpdateConcurrencyExceptionExtensions.ClientWins(ex);
            }

            return RedirectToAction("Index", "Group");
        }
  
        public ActionResult AddBandMember(int userId)
        {
            AddBandMemberToSession(userId);
            return PartialView("_MusicianTable");
        }

        public ActionResult RemoveBandMember(int userId)
        {
            RemoveBandMemberFromSession(userId);
            return PartialView("_MusicianTable");
        }

        // TODO: Add a Session wrapper class to manipulate session stored variables.

        private bool AddBandMemberToSession(BandMemberModel bandMember)
        {
            var bandMembers = (List<BandMemberModel>)Session["BandMembers"];

            if (bandMembers.Any(x => x.User_ID == bandMember.User_ID)) { return false; }

            bandMembers.Add(bandMember);
            Session["BandMembers"] = bandMembers;

            return true;
        }

        private bool AddBandMemberToSession(int userId)
        {
            var bandMembers = (List<BandMemberModel>)Session["BandMembers"];

            if (bandMembers.Any(x => x.User_ID == userId)) { return false; }

            var member = (from users in db.Users
                          where users.User_ID == userId
                          select new BandMemberModel
                          {
                              User_ID = users.User_ID,
                              FirstName = users.FirstName,
                              LastName = users.LastName,
                              Location = users.Location
                          }).FirstOrDefault();

            bandMembers.Add(member);
            Session["BandMembers"] = bandMembers;

            return true;
        }

        private bool RemoveBandMemberFromSession(int userId)
        {
            var bandMembers = (List<BandMemberModel>)Session["BandMembers"];

            if (bandMembers.All(x => x.User_ID != userId))
            {
                return false;
            }

            var memberToRemove = bandMembers.FirstOrDefault(x => x.User_ID == userId);
            bandMembers.Remove(memberToRemove);
            Session["BandMembers"] = bandMembers;

            return true;
        }

        private void InitialiseSessionForBandMembers()
        {
            Session["BandMembers"] = new List<BandMemberModel>();
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

        private bool CurrentUserIsAuthenticated()
        {
            return System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
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

        private BandMemberModel GetAuthenticatedUserToBandMemberModel()
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

            return Query.FirstOrDefault();
        }

        public ActionResult SearchMusician(string searchString)
        {
            var musicians = UserDao.SearchBandMembers(searchString);
            BandMemberModel bandMembers = new BandMemberModel();

            foreach (var musician in musicians)
            {
                bandMembers.BandMembers.Add(musician);
            }

            return PartialView("_MusicianFinder", bandMembers);
        }
    }
}
