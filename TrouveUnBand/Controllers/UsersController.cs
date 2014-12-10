using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using TrouveUnBand.Models;
using System.Drawing;
using TrouveUnBand.Classes;
using TrouveUnBand.POCO;

namespace TrouveUnBand.Controllers
{
    public class UsersController : Controller
    {
        private TrouveUnBandEntities db = new TrouveUnBandEntities();

        public ActionResult NewProfilePage()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User userModel)
        {
            var returnCode = "";
            if (ModelState.IsValid)
            {
                if (userModel.Password == userModel.ConfirmPassword)
                {
                    returnCode = Insertcontact(userModel);
                    if (returnCode == "")
                    {
                        TempData["success"] = AlertMessages.REGISTRATION_CONFIRMED;
                        FormsAuthentication.SetAuthCookie(userModel.Nickname, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    returnCode = "Le mot de passe et sa confirmation ne sont pas identiques.";
                }
            }

            TempData["TempDataError"] = returnCode;
            return View();
        }

        private string Insertcontact(User userbd)
        {
            try
            {
                var validUserQuery = (from user in db.Users
                                      where
                                      user.Email.Equals(userbd.Email) ||
                                      user.Nickname.Equals(userbd.Nickname)
                                      select new SearchUserInfo
                                      {
                                          Nickname = user.Nickname,
                                          Email = user.Email
                                      }).FirstOrDefault();

                if (validUserQuery == null)
                {
                    userbd.Photo = Photo.USER_STOCK_PHOTO_MALE;
                    if(userbd.Gender == "Femme")
                    {
                        userbd.Photo = Photo.USER_STOCK_PHOTO_FEMALE;
                    }

                    userbd.Password = Encrypt(userbd.Password);
                    userbd = Geolocalisation.SetUserLocation(userbd);

                    db.Database.Connection.Open();
                    db.Users.Add(userbd);
                    db.SaveChanges();
                    db.Database.Connection.Close();

                    return "";
                }

                return "L'utilisateur existe déjà";
            }
            catch(Exception ex)
            {
                return "Une erreur interne s'est produite. Veuillez réessayer plus tard.";
            }
        }

        private string Encrypt(string password)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(password ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string returnLoginValid = LoginValid(model.Nickname, model.Password);
                if (returnLoginValid != "")
                {
                    model.Nickname = returnLoginValid;
                    FormsAuthentication.SetAuthCookie(model.Nickname, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                TempData["TempDataError"] = "Votre identifiant/courriel ou mot de passe est incorrect. S'il vous plait, veuillez réessayer.";
                return View();
            }
            TempData["TempDataError"] = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private String LoginValid(string nicknameOrEmail, string password)
        {
            try
            {
                string encryptedPass = Encrypt(password);
                var loginQuery = (from user in db.Users
                                  where
                                  (user.Email.Equals(nicknameOrEmail) ||
                                  user.Nickname.Equals(nicknameOrEmail)) &&
                                  user.Password.Equals(encryptedPass)
                                  select new LoginModel
                                  {
                                      Nickname = user.Nickname,
                                      Email = user.Email,
                                      Password = user.Password
                                  }).FirstOrDefault();
                if (loginQuery != null)
                {
                    return loginQuery.Nickname;
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        private string UpdateProfil(User user)
        {
            try
            {
                var loggedOnUser = db.Users.FirstOrDefault(x => x.Nickname == user.Nickname);
                SetUserInfo(loggedOnUser, user);
                db.SaveChanges();

                return "";
            }
            catch
            {
                return "Une erreur interne s'est produite. Veuillez réessayer plus tard";
            }
        }

        public ActionResult ProfileModification()
        {
            ViewBag.InstrumentListDD = new List<Instrument>(db.Instruments);

            User loggedOnUserValid = GetUserInfo(User.Identity.Name);

            ViewData["UserData"] = loggedOnUserValid;

            User musicianQuery = db.Users.FirstOrDefault(x => x.User_ID == loggedOnUserValid.User_ID);
            if (musicianQuery == null)
            {
                musicianQuery = new User();
            }
            ViewData["MusicianProfilData"] = musicianQuery;
            return View();
        }

        [HttpPost]
        public ActionResult UserProfileModification(User userModel)
        {
            userModel.Nickname = User.Identity.Name;

            var returnCode = UpdateProfil(userModel);

            if (returnCode == "")
            {
                TempData["success"] = "Le profil a été mis à jour.";
                return RedirectToAction("Index", "Home");
            }

            TempData["TempDataError"] = "Une erreur interne s'est produite";
            return RedirectToAction("ProfileModification", "Users");
        }

        [HttpPost]
        public ActionResult MusicianProfileModification(User user)
        {
            string instrumentList = Request["InstrumentList"];
            string[] instrumentArray = instrumentList.Split(',');
            bool isUpdated;

            if (AllUnique(instrumentArray))
            {
                user = db.Users.FirstOrDefault(x => x.Nickname == User.Identity.Name);
                string skillList = Request["SkillsList"];
                string[] skillArray = skillList.Split(',');
                string descriptionMusician = Request["TextArea"];

                user.Description = descriptionMusician;
                user.Users_Instruments.Clear();

                for (int i = 0; i < instrumentArray.Length; i++)
                {
                    var userInstruments = new Users_Instruments();
                    int currentInstrumentId = Convert.ToInt32(instrumentArray[i]);
                    var instrument = db.Instruments.FirstOrDefault(x => x.Instrument_ID == currentInstrumentId);

                    userInstruments.Instrument_ID = instrument.Instrument_ID;
                    userInstruments.Skills = Convert.ToInt32(skillArray[i]);
                    userInstruments.User_ID = user.User_ID;

                    user.Users_Instruments.Add(userInstruments);
                }

                isUpdated = SaveUpdatedUser();

                if (isUpdated)
                {
                    TempData["success"] = "Le profil musicien a été mis à jour.";
                    return RedirectToAction("Index", "Home");
                }

                TempData["TempDataError"] = "Une erreur interne s'est produite";
                return RedirectToAction("ProfileModification", "Users");
            }

            TempData["TempDataError"] = "Vous ne pouvez pas entrer deux fois le même instrument";
            return RedirectToAction("ProfileModification", "Users");
        }

        private bool AllUnique(string[] array)
        {
            bool allUnique = array.Distinct().Count() == array.Length;
            return allUnique;
        }

        private User GetUserInfo(string nickname)
        {
            User loggedOnUser = db.Users.FirstOrDefault(x => x.Nickname == nickname);
            loggedOnUser.Photo = loggedOnUser.Photo;
            return loggedOnUser;
        }

        public string GetUserFullName()
        {
            var loggedOnUser = GetUserInfo(User.Identity.Name);
            return loggedOnUser.FirstName + " " + loggedOnUser.LastName;
        }

        public string GetPhotoSrc()
        {
            var loggedOnUser = GetUserInfo(User.Identity.Name);
            return loggedOnUser.Photo;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CropImage(User userWithPhoto)
        {
            var postedPhoto = Request.Files[0];

            if (postedPhoto.ContentLength == 0)
            {
                TempData["TempDataError"] = AlertMessages.POSTED_FILES_ERROR;
                return RedirectToAction("ProfileModification");
            }

            try
            {
                User loggedOnUser = db.Users.FirstOrDefault(x => x.Nickname == User.Identity.Name);

                if(!Photo.IsPhoto(postedPhoto))
                {
                    TempData["TempDataError"] = AlertMessages.FILE_TYPE_INVALID;
                    return RedirectToAction("ProfileModification");
                }

                Image image = Image.FromStream(postedPhoto.InputStream, true, true);

                if (image.Width < 250 || image.Height < 172 || image.Width > 800 || image.Height > 600)
                {
                    image = PhotoResizer.ResizeImage(image, 250, 172, 800, 600);
                }

                var croppedPhoto = PhotoCropper.CropImage(image, userWithPhoto.ProfilePicture.CropRect);
                var savedPhotoPath = FileHelper.SavePhoto(croppedPhoto, loggedOnUser.Nickname, FileHelper.Category.USER_PROFILE_PHOTO);
                loggedOnUser.Photo = savedPhotoPath;
                db.SaveChanges();

                TempData["success"] = AlertMessages.PICTURE_CHANGED;
                return RedirectToAction("ProfileModification", "Users");
            }
            catch
            {
                TempData["TempDataError"] = AlertMessages.INTERNAL_ERROR;
                return RedirectToAction("ProfileModification");
            }
        }

        private void SetUserInfo(User currentUser, User newUser)
        {
            if ((currentUser.Latitude == 0.0 || currentUser.Longitude == 0.0) || currentUser.Location != newUser.Location)
            {
                currentUser.Location = newUser.Location;
                currentUser = Geolocalisation.SetUserLocation(currentUser);
            }

            currentUser.FirstName = newUser.FirstName;
            currentUser.LastName = newUser.LastName;
            currentUser.BirthDate = newUser.BirthDate;
            currentUser.Email = newUser.Email;

            if (currentUser.Gender != newUser.Gender &&
                currentUser.Photo.Contains("_stock_user_"))
            {
                currentUser.Photo = Photo.USER_STOCK_PHOTO_MALE;
                if(newUser.Gender == "Femme")
                {
                    currentUser.Photo = Photo.USER_STOCK_PHOTO_FEMALE;
                }
            }
            currentUser.Gender = newUser.Gender;
        }

        private bool SaveUpdatedUser()
        {
            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
