using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using TrouveUnBand.Models;
using System.Drawing;
using TrouveUnBand.Classes;

namespace TrouveUnBand.Controllers
{
    public class UsersController : Controller
    {
        private TrouveUnBandEntities db = new TrouveUnBandEntities();
        
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
            string RC = "";
            if (ModelState.IsValid)
            {
                if (userModel.Password == userModel.ConfirmPassword)
                {
                    RC = Insertcontact(userModel);
                    if (RC == "")
                    {
                        TempData["success"] = POCO.AlertMessages.REGISTRATION_CONFIRMED;
                        FormsAuthentication.SetAuthCookie(userModel.Nickname, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    RC = "Le mot de passe et sa confirmation ne sont pas identiques.";
                }
            }

            TempData["TempDataError"] = RC;
            return View();
        }

        private string Insertcontact(User userbd)
        {
            try
            {
                var ValidUserQuery = (from User in db.Users
                                      where
                                      User.Email.Equals(userbd.Email) ||
                                      User.Nickname.Equals(userbd.Nickname)
                                      select new SearchUserInfo
                                      {
                                          Nickname = User.Nickname,
                                          Email = User.Email
                                      }).FirstOrDefault();

                if (ValidUserQuery == null)
                {
                    CreateUser(userbd);
                    db.Database.Connection.Open();
                    db.Users.Add(userbd);
                    db.SaveChanges();
                    db.Database.Connection.Close();
                    return "";
                }
                else
                {
                    return "L'utilisateur existe déjà";
                }
            }
            catch
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
                string ReturnLoginValid = LoginValid(model.Nickname, model.Password);
                if (ReturnLoginValid != "")
                {
                    model.Nickname = ReturnLoginValid;
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

        private String LoginValid(string NicknameOrEmail, string Password)
        {
            try
            {
                string EncryptedPass = Encrypt(Password);
                var LoginQuery = (from User in db.Users
                                  where
                                  (User.Email.Equals(NicknameOrEmail) ||
                                  User.Nickname.Equals(NicknameOrEmail)) &&
                                  User.Password.Equals(EncryptedPass)
                                  select new LoginModel
                                  {
                                      Nickname = User.Nickname,
                                      Email = User.Email,
                                      Password = User.Password
                                  }).FirstOrDefault();
                if (LoginQuery != null)
                {
                    return LoginQuery.Nickname;
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
                var LoggedOnUser = db.Users.FirstOrDefault(x => x.Nickname == user.Nickname);
                SetUserInfo(LoggedOnUser, user);
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

            User LoggedOnUserValid = GetUserInfo(User.Identity.Name);

            ViewData["UserData"] = LoggedOnUserValid;

            User MusicianQuery = db.Users.FirstOrDefault(x => x.User_ID == LoggedOnUserValid.User_ID);
            if (MusicianQuery == null)
            {
                MusicianQuery = new User();
            }
            ViewData["MusicianProfilData"] = MusicianQuery;
            return View();
        }

        [HttpPost]
        public ActionResult UserProfileModification(User userModel)
        {
            userModel.Nickname = User.Identity.Name;
            string RC = "";

            RC = UpdateProfil(userModel);

            if (RC == "")
            {
                TempData["success"] = "Le profil a été mis à jour.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["TempDataError"] = "Une erreur interne s'est produite";
                return RedirectToAction("ProfileModification", "Users");
            }
        }

        [HttpPost]
        public ActionResult MusicianProfileModification(User user)
        {
            string InstrumentList = Request["InstrumentList"];
            string[] InstrumentArray = InstrumentList.Split(',');
            string RC;

            if (AllUnique(InstrumentArray))
            {
                string SkillList = Request["SkillsList"];
                string[] SkillArray = SkillList.Split(',');
                string DescriptionMusician = Request["TextArea"];

                user.Description = DescriptionMusician;
                for (int i = 0; i < InstrumentArray.Length; i++)
                {
                    int currentInstrumentID = Convert.ToInt32(InstrumentArray[i]);
                    var instrument = db.Instruments.FirstOrDefault(x => x.Instrument_ID == currentInstrumentID);
                    Users_Instruments UserInstruments = new Users_Instruments();

                    UserInstruments.Instrument_ID = instrument.Instrument_ID;
                    UserInstruments.Skills = Convert.ToInt32(SkillArray[i]);
                    UserInstruments.User_ID = user.User_ID;

                    user.Users_Instruments.Add(UserInstruments);
                }

                RC = UpdateMusicianProfile(user);
                if (RC == "")
                {
                    TempData["success"] = "Le profil musicien a été mis à jour.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["TempDataError"] = "Une erreur interne s'est produite";
                    return RedirectToAction("ProfileModification", "Users");
                }
            }
            else
            {
                TempData["TempDataError"] = "Vous ne pouvez pas entrer deux fois le même instrument";
                return RedirectToAction("ProfileModification", "Users");
            }

        }

        private bool AllUnique(string[] array)
        {
            bool allUnique = array.Distinct().Count() == array.Length;
            return allUnique;
        }

        private User GetUserInfo(string Nickname)
        {
            User LoggedOnUser = db.Users.FirstOrDefault(x => x.Nickname == Nickname);
            LoggedOnUser.Photo = LoggedOnUser.Photo;
            return LoggedOnUser;
        }

        public string GetUserFullName()
        {
            var LoggedOnUser = GetUserInfo(User.Identity.Name);
            return LoggedOnUser.FirstName + " " + LoggedOnUser.LastName;
        }

        public string GetPhotoSrc()
        {
            var LoggedOnUser = GetUserInfo(User.Identity.Name);
            Photo profilePhoto = new Photo();
            profilePhoto.PhotoArray = LoggedOnUser.Photo;
            return profilePhoto.PhotoSrc;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CropImage(User UserPicture)
        {
            var PostedPhoto = Request.Files[0];

            if (PostedPhoto.ContentLength == 0)
            {
                TempData["TempDataError"] = POCO.AlertMessages.POSTED_FILES_ERROR;
                return RedirectToAction("ProfileModification");
            }

            try
            {
                User LoggedOnUser = db.Users.FirstOrDefault(x => x.Nickname == User.Identity.Name);

                if(!Photo.IsPhoto(PostedPhoto))
                {
                    TempData["TempDataError"] = POCO.AlertMessages.FILE_TYPE_INVALID;
                    return RedirectToAction("ProfileModification");
                }

                Image image = Image.FromStream(PostedPhoto.InputStream, true, true);

                if (image.Height < 172 || image.Width < 250 || image.Height > 413 || image.Width > 600)
                {
                    image = PhotoResizer.ResizeImage(image, 172, 250, 413, 600);
                }

                byte[] croppedPhoto = PhotoCropper.CropImage(image, UserPicture.ProfilePicture.CropRect);

                LoggedOnUser.Photo = croppedPhoto;
                db.SaveChanges();

                TempData["success"] = POCO.AlertMessages.PICTURE_CHANGED;
                return RedirectToAction("ProfileModification", "Users");
            }
            catch
            {
                TempData["TempDataError"] = POCO.AlertMessages.INTERNAL_ERROR;
                return RedirectToAction("ProfileModification");
            }
        }

        private User CreateUser(User userToCreate)
        {
            userToCreate.Photo = Photo.StockPhoto;
            userToCreate.Password = Encrypt(userToCreate.Password);
            userToCreate = Geolocalisation.SetUserLocation(userToCreate);

            return userToCreate;
        }

        private User SetUserInfo(User currentUser, User newUser)
        {
            if ((currentUser.Latitude == 0.0 || currentUser.Longitude == 0.0) || currentUser.Location != newUser.Location)
            {
                currentUser.Location = newUser.Location;
                currentUser = Geolocalisation.SetUserLocation(currentUser);
            }

            currentUser.FirstName = newUser.FirstName;
            currentUser.LastName = newUser.LastName;
            currentUser.BirthDate = newUser.BirthDate;
            currentUser.Gender = newUser.Gender;
            currentUser.Email = newUser.Email;

            return currentUser;
        }

        private string UpdateMusicianProfile(User userToUpdate)
        {
            try
            {
                var LoggedOnUser = db.Users.FirstOrDefault(x => x.Nickname == User.Identity.Name);
                SetMusicianInfo(LoggedOnUser, userToUpdate);
                db.SaveChanges();

                return "";
            }
            catch
            {
                return "Une erreur interne s'est produite. Veuillez réessayer plus tard";
            }
        }

        private User SetMusicianInfo(User currentUser, User newUser)
        {
            currentUser.Users_Instruments = newUser.Users_Instruments;
            currentUser.Description = newUser.Description;

            return currentUser;
        }
    }
}
