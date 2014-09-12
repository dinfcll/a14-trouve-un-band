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
        //
        // GET: /Users/


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
        public ActionResult Register(User u)
        {
            if (ModelState.IsValid)
            {
                if (u.Password == u.ConfirmPassword)
                { 
                    insertcontact(u);
                    TempData["notice"] = "Registration Confirmed"; 
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["PasswordNotEqual"] = "Both password fields must be identical";
                    return View();
                }
            }
            return View();
        }

        private void insertcontact(User u)
        {
            //TODO : Insertion BD
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=tempdb;Integrated Security=True";
            try
            {
                myConnection.Open();
                String query = String.Format("INSERT INTO Users(FirstName, LastName, BirthDate, Nickname, Email, Password, City) Values ('{0}','{1}',convert(datetime,'{2}'),'{3}','{4}',HASHBYTES('SHA1','{5}'),'{6}')", u.FirstName, u.LastName, u.BirthDate, u.Nickname, u.Email, u.Password, u.City);
                SqlCommand myCommand1 = new SqlCommand(query, myConnection);
                myCommand1.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                myConnection.Close();
            }
        }

        private string EncryptPassword(string password) // note
        {
            byte[] pass = Encoding.UTF8.GetBytes(password);
            /*MD5 encpwrd = new MD5CryptoServiceProvider();
            return Encoding.UTF8.GetString(encpwrd.ComputeHash(pass));*/
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] finalpass=sha.ComputeHash(pass);
            string test = Encoding.UTF8.GetString(finalpass);
            return test;
        }

        [HttpPost]
        public ActionResult Login(LoginModel model) //note
        {
            if (LoginValid(model.Nickname, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Nickname, model.RememberMe);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
                //code erreur
            }

            return View();
        }

        private bool LoginValid(string nickname,string password) // note
        {
            String query;
            SqlCommand myCommand;
            SqlConnection myConnection = new SqlConnection();
            SqlDataReader reader;
            myConnection.ConnectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=tempdb;Integrated Security=True";
            try
            {
                myConnection.Open();
                query = String.Format("SELECT [Nickname],[Password] FROM Users WHERE Nickname='{0}'",nickname);
                myCommand = new SqlCommand(query, myConnection);
                reader = myCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    //password = EncryptPassword(password); a encrypter
                    reader.Read();
                    if (reader[1].ToString()==password)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
}
