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

        public ActionResult ProfileModification()
        {
            User LoggedOnUser = GetUserInfo(User.Identity.Name);
            ViewData["UserData"] = LoggedOnUser;
            return View();
        }

        private SqlConnection ConnectionDB()
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True";
            return myConnection;
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            string RC="";
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

        private User GetUserInfo(string Nickname)
        {
            User LoggedOnUser = new User();
            String query;
            SqlCommand myCommand;
            SqlConnection myConnection = ConnectionDB();
            SqlDataReader reader;
            try
            {
                myConnection.Open();
                query = String.Format("SELECT FirstName, LastName, BirthDate, Nickname, Email, City FROM Users WHERE Nickname='{0}'", Nickname);
                myCommand = new SqlCommand(query, myConnection);
                reader = myCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    LoggedOnUser.FirstName = reader.GetString(0);
                    LoggedOnUser.LastName = reader.GetString(1);
                    LoggedOnUser.BirthDate = reader.GetDateTime(2).ToString("yyyy/MM/dd");
                    LoggedOnUser.Nickname = reader.GetString(3);
                    LoggedOnUser.Email = reader.GetString(4);
                    LoggedOnUser.City = reader.GetString(5);
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
    }
}
