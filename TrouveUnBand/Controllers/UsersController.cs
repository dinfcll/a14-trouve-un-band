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

namespace TrouveUnBand.Controllers
{
    public class UsersController : Controller
    {
        //DbContext db = new DbContext("Data Source=localhost;Initial Catalog=tempdb;Integrated Security=True");
        private TrouveUnBand.Models.DBModels.DBTUBContext db = new TrouveUnBand.Models.DBModels.DBTUBContext();
        
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
                        TempData["notice"] = "Registration Confirmed";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    RC = "Both password fields must be identical";
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
                                      User.Nickname.Equals(user.Email)
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
            catch (Exception e)
            {
                return "Une erreur interne s'est produite. Veuillez réessayer plus tard";
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

            TempData["LoginFail"] = "Your nickname/email or password is incorrect. Please try again.";
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
            catch (Exception e)
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
                LoggedOnUser.City = user.City;
                LoggedOnUser.Email = user.Email;
                LoggedOnUser.FirstName = user.FirstName;
                LoggedOnUser.Photo = user.Photo;
                db.SaveChanges();
                return "";
            }
            catch (Exception e)
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
                user.Photo = StockPhoto();
                RC = Updatecontact(user);
            }
            else
            {
                HttpPostedFileBase PostedPhoto = Request.Files[0];
                Image img = Image.FromStream(PostedPhoto.InputStream, true, true);
                byte[] bd = imageToByteArray(img);
                user.PhotoName = PostedPhoto.FileName;
                user.Photo = bd;
                RC = Updatecontact(user);
            }


            if (RC == "")
            {
                TempData["notice"] = "Profil mis à jour";
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

        public ActionResult ProfilePic(string nickname)
        {
            try
            {
                var PicQuery = (from User in db.User
                                where
                                User.Nickname.Equals(nickname)
                                select new Photo
                                {
                                    ProfilePicture = User.Photo
                                }).FirstOrDefault();

                return File(PicQuery.ProfilePicture, "Image/jpeg");
            }
            catch (Exception ex)
            {
                TempData["TempDataError"] = "Erreur lors du chargement de la photo de profil";
                return RedirectToAction("Index", "Home");
            }
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public byte[] StockPhoto()
        {
            string path = HttpContext.Server.MapPath("~/Images/stock_user.jpg");
            Image stock = Image.FromFile(path);
            return imageToByteArray(stock);
        }
    }
}
