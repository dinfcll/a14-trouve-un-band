using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrouveUnBand.Models;
using WebMatrix.WebData;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Web.Script.Serialization;
using System.Net.Http;

namespace TrouveUnBand.Controllers
{
    public class UsersController : Controller
    {
        private TrouveUnBand.Models.DBModels.DBTUBContext db = new TrouveUnBand.Models.DBModels.DBTUBContext();
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CalculLocation()
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
        public ActionResult Register(User user)
        {
            string RC = "";
            if (ModelState.IsValid)
            {
                if (user.Password == user.ConfirmPassword)
                {
                    user.Photo = StockPhoto();
                    RC = Insertcontact(user);
                    if (RC == "")
                    {
                        TempData["success"] = "L'inscription est confirmée!";
                        FormsAuthentication.SetAuthCookie(user.Nickname, false);
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

        private string Insertcontact(User user)
        {
            try
            {
                var ValidUserQuery = (from User in db.User
                                      where
                                      User.Email.Equals(user.Email) ||
                                      User.Nickname.Equals(user.Nickname)
                                      select new SearchUserInfo
                                      {
                                          Nickname = User.Nickname,
                                          Email = User.Email
                                      }).FirstOrDefault();

                if (ValidUserQuery == null)
                {
                    db.Database.Connection.Open();
                    user.Password = Encrypt(user.Password);
                    db.User.Add(user);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private String LoginValid(string NicknameOrEmail,string Password)
        {
            try
            {
                string EncryptedPass = Encrypt(Password);
                var LoginQuery = (from User in db.User
                                    where
                                    (User.Email.Equals(NicknameOrEmail) ||
                                    User.Nickname.Equals(NicknameOrEmail)) &&
                                    User.Password.Equals(EncryptedPass)
                                    select new Login
                                    {
                                        Nickname = User.Nickname,
                                        Email = User.Email,
                                        Password = User.Password
                                    }).FirstOrDefault();
                if (LoginQuery != null)
                {
                    return LoginQuery.Nickname;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        private string Updatecontact(User user)
        {
            try
            {
                User LoggedOnUser = db.User.FirstOrDefault(x => x.Nickname == user.Nickname);
                LoggedOnUser.LastName = user.LastName;
                LoggedOnUser.Location = user.Location;
                LoggedOnUser.BirthDate = user.BirthDate;
                LoggedOnUser.Email = user.Email;
                LoggedOnUser.FirstName = user.FirstName;
                LoggedOnUser.Photo = user.Photo;
                LoggedOnUser.Gender = user.Gender;
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
            User LoggedOnUser = GetUserInfo(User.Identity.Name);
            if (LoggedOnUser.Photo != null)
            {
                LoggedOnUser.PhotoName = "data:image/jpeg;base64," + Convert.ToBase64String(LoggedOnUser.Photo);
            }
            ViewData["UserData"] = LoggedOnUser;
            return View();
        }

        [HttpPost]
        public ActionResult ProfileModification(User user)
        {
            user.Nickname = User.Identity.Name;
            string RC = "";
            if (Request.Files[0].ContentLength == 0)
            {
                user.Photo = GetProfilePicByte(user.Nickname);
                RC = Updatecontact(user);
            }
            else
            {
                HttpPostedFileBase PostedPhoto = Request.Files[0];
                try
                {
                    Image img = Image.FromStream(PostedPhoto.InputStream, true, true);
                    byte[] bytephoto = imageToByteArray(img);
                    user.PhotoName = PostedPhoto.FileName;
                    user.Photo = bytephoto;
                }
                catch
                {
                    user.Photo = StockPhoto();
                }
                RC = Updatecontact(user);
            }

            if (RC == "")
            {
                TempData["success"] = "Le profil a été mis à jour.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["TempDataError"] = "Une erreur interne s'est produite";
                return View();
            }

        }

        private User GetUserInfo(string Nickname)
        {
            User LoggedOnUser = db.User.FirstOrDefault(x => x.Nickname == Nickname);
            return LoggedOnUser;
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public byte[] StockPhoto()
        {
            string path = HttpContext.Server.MapPath("~/Images/stock_user.jpg");
            Image stock = Image.FromFile(path);
            return imageToByteArray(stock);
        }

        public byte[] GetProfilePicByte(string nickname)
        {
            var PicQuery = (from User in db.User
                            where
                            User.Nickname.Equals(nickname)
                            select new Photo
                            {
                                ProfilePicture = User.Photo
                            }).FirstOrDefault();
            return PicQuery.ProfilePicture;
        }

        public string GetUserFullName()
        {
            User LoggedOnUser = GetUserInfo(User.Identity.Name);
            return LoggedOnUser.FirstName + " " + LoggedOnUser.LastName;
        }

        public string GetPhotoName()
        {
            User LoggedOnUser = GetUserInfo(User.Identity.Name);
            string PhotoName = "data:image/jpeg;base64," + Convert.ToBase64String(LoggedOnUser.Photo);
            return PhotoName;

        }

        public string GetLocation(string Location)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("https://maps.googleapis.com");

            var response = client.GetAsync("/maps/api/geocode/json?address=" + Location + ",+CA&key=AIzaSyAzPU-uqEi7U9Ry15EgLAVZ03_4rbms8Ds").Result;

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;

                var location = new JavaScriptSerializer().Deserialize<LocationModels>(responseBody);
                return location.results[location.results.Count - 1].geometry.location.lat.ToString() + 
                       location.results[location.results.Count - 1].geometry.location.lng;
            }
            return "Erreur";
        }

        public string GetDistance(string LatitudeP1,string LongitudeP1, string LatitudeP2, string LongitudeP2)
        {
            bool IsDouble;
            double LatP1=0.0, LngP1=0.0, LatP2=0.0, LngP2=0.0;

            IsDouble = double.TryParse(LatitudeP1,out LatP1);
            if(IsDouble == true)
            {
                IsDouble = double.TryParse(LongitudeP1, out LngP1);
            }
            if (IsDouble == true)
            {
                IsDouble = double.TryParse(LatitudeP2, out LatP2);
            }
            if (IsDouble == true)
            {
                IsDouble = double.TryParse(LongitudeP2, out LngP2);
            }
            if (IsDouble == true)
            {
                double R = 6378.137; // Earth’s mean radius in kilometer
                var lat = ToRadians(LatP2 - LatP1);
                var lng = ToRadians(LngP2 - LngP1);
                var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                              Math.Cos(ToRadians(LatP1)) * Math.Cos(ToRadians(LatP2)) *
                              Math.Sin(lng / 2) * Math.Sin(lng / 2);
                var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
                int d=  (int)(R * h2);
                return d.ToString() + " kilomètres";
            }
            return "Erreur";
        }

        public static double ToRadians(double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}

