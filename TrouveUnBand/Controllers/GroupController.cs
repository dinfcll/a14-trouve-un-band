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
    public class GroupController : BaseController
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
            if (!CurrentUserIsAuthenticated())
                return View("../Shared/Authentication");
            InitialiseSessionForBandMembers();

            var viewModel = new BandCreationViewModel();
            var authUser = GetAuthenticatedUser();

            viewModel.Band.Users.Add(authUser);

            AddBandMemberToSession(new BandMemberModel
            {
                FirstName = authUser.FirstName,
                LastName = authUser.LastName,
                Location = authUser.Location,
                User_ID = authUser.User_ID
            });

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(string json)
        {
            var band = JsonToModel.ToBand(json, db);

            var alreadyExists = CheckIfBandAlreadyExists(band);

            band.UpdateLocationWithAPI();

            try
            {
                db.Bands.Add(band);
                db.SaveChanges();

                Success(Messages.BAND_CREATION_SUCCESS(band),true);
            }
            catch (Exception ex)
            {
                Danger(Messages.INTERNAL_ERROR,true);
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Confirmation(BandCreationViewModel viewModel, string[] cbSelectedGenres)
        {
            var alreadyExists = CheckIfBandAlreadyExists(viewModel.Band);
            if (alreadyExists)
            {
                Warning(Messages.EXISTING_BAND(viewModel.Band));
            }

            foreach (var genreName in cbSelectedGenres)
            {
                var genre = db.Genres.FirstOrDefault(x => x.Name == genreName);
                viewModel.Band.Genres.Add(genre);
            }

            foreach (var musician in (List<BandMemberModel>)Session["BandMembers"])
            {
                var user = db.Users.FirstOrDefault(x => x.User_ID == musician.User_ID);
                viewModel.Band.Users.Add(user);
            }

            return View(viewModel);
        }

        private bool CheckIfBandAlreadyExists(Band band)
        {
            var bandAlreadyExists = db.Bands.Any(x => x.Name == band.Name && x.Location == band.Location);
            return bandAlreadyExists;
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
                Danger(Messages.INTERNAL_ERROR,true);
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

                ((IObjectContextAdapter)db).ObjectContext.Detach(bandCreationModel.Band);

                var bandToUpdate = db.Bands.Single(x => x.Band_ID == bandCreationModel.Band.Band_ID);
                db.Set(typeof(Band)).Attach(bandToUpdate);
                bandToUpdate.Genres.Clear();
                bandToUpdate.Users.Clear();

                foreach (var genreName in cbSelectedGenres)
                {
                    var genre = db.Genres.FirstOrDefault(x => x.Name == genreName);
                    bandToUpdate.Genres.Add(genre);
                }

                foreach (var musician in (List<BandMemberModel>)Session["BandMembers"])
                {
                    var user = db.Users.FirstOrDefault(x => x.User_ID == musician.User_ID);
                    bandToUpdate.Users.Add(user);
                }

                bandToUpdate.UpdateLocationWithAPI();

                db.Entry(bandToUpdate).State = EntityState.Modified;
                db.SaveChanges();

                Success(Messages.BAND_CREATION_SUCCESS(bandCreationModel.Band),true);
            }
            catch (NullReferenceException ex)
            {
                Danger(Messages.INTERNAL_ERROR,true);
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Danger(Messages.INTERNAL_ERROR,true);
                Console.WriteLine(ex.Message);
            }
            catch (DbEntityValidationException ex)
            {
                Danger(Messages.INTERNAL_ERROR,true);
                Console.WriteLine(ex.Message);
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
                Danger(Messages.NOT_CONNECTED, true);
            }
            var userName = System.Web.HttpContext.Current.User.Identity.Name;
            var authenticatedUser = db.Users.FirstOrDefault(x => x.Nickname == userName);

            return authenticatedUser;
        }

        private BandMemberModel GetAuthenticatedUserToBandMemberModel()
        {
            if (!CurrentUserIsAuthenticated())
            {
                Danger(Messages.NOT_CONNECTED, true);
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