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

        private SqlConnection ConnectionDB()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = "Data Source=localhost;Initial Catalog=tempdb;Integrated Security=True";
            return myConnection;
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            string RC = "";
            if (ModelState.IsValid)
            {
                if (user.Password == user.ConfirmPassword)
                {
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
                var ValidUserQuery = from User in db.User
                                      where
                                      User.Email.Contains(user.Email) ||
                                      User.Nickname.Contains(user.Email)
                                      select new SearchUserInfo
                                      {
                                          Nickname = User.Nickname,
                                          Email = User.Email
                                      };

                List<SearchUserInfo> ValidateEmailNickName = new List<SearchUserInfo>();
                ValidateEmailNickName.AddRange(ValidUserQuery);
                Predicate<SearchUserInfo> PredEmail = (x => x.Email == user.Email);
                Predicate<SearchUserInfo> PredNick = (x => x.Nickname == user.Nickname);
                if (ValidateEmailNickName.Exists(PredEmail) == false && ValidateEmailNickName.Exists(PredNick) == false)
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
                var LoginQuery = from User in db.User
                                    where
                                    (User.Email.Contains(NicknameOrEmail) ||
                                    User.Nickname.Contains(NicknameOrEmail)) &&
                                    User.Password.Contains(EncryptedPass)
                                    select new Login
                                    {
                                        Nickname = User.Nickname,
                                        Email = User.Email,
                                        Password = User.Password
                                    };
                List<Login> ValidateLogin = new List<Login>();
                ValidateLogin.AddRange(LoginQuery);
                Predicate<Login> PredEmail = (x => x.Email == NicknameOrEmail);
                Predicate<Login> PredNick = (x => x.Nickname == NicknameOrEmail);
                Predicate<Login> PredPass = (x => x.Password == Encrypt(Password));

                if ((ValidateLogin.Exists(PredEmail) == true || ValidateLogin.Exists(PredNick) == true) && ValidateLogin.Exists(PredPass) == true)
                {
                    if (ValidateLogin.Exists(PredEmail) == true)
                    {
                        return ValidateLogin.Find(PredEmail).Nickname.ToString();
                    }
                    else
                    {
                        return ValidateLogin.Find(PredNick).Nickname.ToString();
                    }
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

        private string Updatecontact(User user, bool image)
        {
            SqlConnection myConnection = ConnectionDB();
            try
            {
                myConnection.Open();
                String query;
                SqlCommand myCommand;
                if (image == false)
                {
                    query = String.Format("UPDATE Users SET FirstName='{0}', LastName='{1}', BirthDate=convert(datetime,'{2}',111),"
                    + "Email='{3}', City='{4}' where Nickname = '{5}'",
                    user.FirstName, user.LastName, user.BirthDate, user.Email, user.City, User.Identity.Name);
                    myCommand = new SqlCommand(query, myConnection);
                }
                else
                {
                    query = String.Format("UPDATE Users SET FirstName='{0}', LastName='{1}', BirthDate=convert(datetime,'{2}',111),"
                    + "Email='{3}', City='{4}', Photo=CONVERT(VARBINARY(Max),@TEST) where Nickname = '{5}'",
                    user.FirstName, user.LastName, user.BirthDate, user.Email, user.City, User.Identity.Name);
                    myCommand = new SqlCommand(query, myConnection);
                    myCommand.Parameters.AddWithValue("@TEST", user.Photo);
                }
                myCommand.ExecuteNonQuery();
                return "";
            }
            catch (Exception e)
            {
                return "Une erreur interne s'est produite. Veuillez réessayer plus tard";
            }
            finally
            {
                myConnection.Close();
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
            string RC = "";
            if (Request.Files[0].ContentLength == 0)
            {
                RC = Updatecontact(user, false);
            }
            else
            {
                HttpPostedFileBase PostedPhoto = Request.Files[0];
                Image img = Image.FromStream(PostedPhoto.InputStream, true, true);
                byte[] bd = imageToByteArray(img);
                user.PhotoName = PostedPhoto.FileName;
                user.Photo = bd;
                RC = Updatecontact(user, true);
            }


            if (RC == "")
            {
                TempData["notice"] = "Profil mis à jour";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                RC = "Une erreur interne s'est produite";
            }
            TempData["TempDataError"] = RC;
            return View();
        }

        private User GetUserInfo(string Nickname)
        {
            User LoggedOnUser = db.User.FirstOrDefault(x => x.Nickname == Nickname);
            return LoggedOnUser;
        }

        public ActionResult ProfilePic(string nickname)
        {
            SqlConnection myConnection = ConnectionDB();
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
    }
}
