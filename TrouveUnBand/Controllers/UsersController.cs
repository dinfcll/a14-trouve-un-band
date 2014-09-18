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

namespace TrouveUnBand.Controllers
{
    public class UsersController : Controller
    {
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
            SqlConnection myConnection = ConnectionDB();
            try
            {
                myConnection.Open();
                String query = String.Format("SELECT [Nickname],[Email] FROM Users WHERE Nickname='{0}' OR Email='{1}'", user.Nickname, user.Email);
                SqlCommand myCommand = new SqlCommand(query, myConnection);
                SqlDataReader reader = myCommand.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    query = String.Format("INSERT INTO Users(FirstName, LastName, BirthDate, Nickname, Email, Password, City) " +
                    "Values ('{0}','{1}',convert(datetime,'{2}',111),'{3}','{4}','{5}','{6}')",
                    user.FirstName, user.LastName, user.BirthDate, user.Nickname, user.Email, Encrypt(user.Password), user.City);

                    myCommand = new SqlCommand(query, myConnection);
                    myCommand.ExecuteNonQuery();
                    return "";
                }
                else
                {
                    reader.Close();
                    return "L'utilisateur existe déjà";
                }
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
            String query;
            SqlCommand myCommand;
            SqlConnection myConnection = ConnectionDB();
            SqlDataReader reader;
            try
            {
                myConnection.Open();
                query = String.Format("SELECT [Nickname],[Password] FROM Users WHERE Nickname='{0}' OR Email='{0}'", NicknameOrEmail);
                myCommand = new SqlCommand(query, myConnection);
                reader = myCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (reader[1].ToString() == Encrypt(Password))
                    {
                        return reader[0].ToString();
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
            finally
            {
                myConnection.Close();
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
                    myCommand.Parameters.AddWithValue("@TEST", user.PhotoByte);
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

        public ActionResult ProfilePic(string nickname)
        {
            SqlConnection myConnection = ConnectionDB();
            try
            {
                myConnection.Open();
                String query = String.Format("SELECT [Photo] FROM Users WHERE Nickname='{0}'", nickname);
                SqlCommand myCommand = new SqlCommand(query, myConnection);
                SqlDataReader reader = myCommand.ExecuteReader();
                return File((byte[])reader.GetSqlBinary(0), "Image/jpeg");
            }
            catch (Exception ex)
            {

            }
            finally
            {
                myConnection.Close();
            }
            return View();
        }

        public ActionResult ProfileModification()
        {
            User LoggedOnUser = GetUserInfo(User.Identity.Name);
            if (LoggedOnUser.PhotoByte != null)
            {
                LoggedOnUser.PhotoName = "data:image/jpeg;base64," + Convert.ToBase64String(LoggedOnUser.PhotoByte);
            }
            ViewData["UserData"] = LoggedOnUser;
            return View();
        }

        [HttpPost]
        public ActionResult ProfileModification(User user)
        {
            string RC = "";
            if (Request.Files.Count == 0)
            {
                RC = Updatecontact(user, false);
            }
            else
            {
                HttpPostedFileBase PostedPhoto = Request.Files[0];
                Image img = Image.FromStream(PostedPhoto.InputStream, true, true);
                byte[] bd = imageToByteArray(img);
                user.PhotoName = PostedPhoto.FileName;
                user.PhotoByte = bd;
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
            User LoggedOnUser = new User();
            string query = "";
            SqlCommand myCommand = new SqlCommand();
            SqlConnection myConnection = ConnectionDB();
            SqlDataReader reader;
            try
            {
                myConnection.Open();
                query = String.Format("SELECT FirstName, LastName, BirthDate, Nickname, Email, City, Photo FROM Users WHERE Nickname='{0}'", Nickname);
                myCommand = new SqlCommand(query, myConnection);
                reader = myCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    LoggedOnUser.FirstName = reader.GetString(0);
                    LoggedOnUser.LastName = reader.GetString(1);
                    LoggedOnUser.BirthDate = reader.GetDateTime(2).ToString("yyyy/MM/dd").Replace('-', '/');
                    LoggedOnUser.Nickname = reader.GetString(3);
                    LoggedOnUser.Email = reader.GetString(4);
                    LoggedOnUser.City = reader.GetString(5);
                    var t = reader.GetValue(6);
                    if (t != null)
                    {
                        LoggedOnUser.PhotoByte = (byte[])reader.GetSqlBinary(6);
                    }
                }
                return LoggedOnUser;
            }
            catch (Exception e)
            {
                return LoggedOnUser;
            }
            finally
            {
                myConnection.Close();
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
