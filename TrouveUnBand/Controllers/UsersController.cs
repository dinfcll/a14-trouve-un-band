using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Models;
using System.Data.SqlClient;
using System.Security;
using System.Security.Cryptography;
using System.Text;

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
            myConnection.ConnectionString = "Data Source=localhost;Initial Catalog=tempdb;Integrated Security=True";
            try
            {
                myConnection.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            SqlCommand myCommand1 = new SqlCommand("INSERT INTO Users(FirstName, LastName, BirthDate, Nickname, Email, Password, City) Values ('" + u.FirstName + "','" + u.LastName + "',convert(datetime,'" + u.BirthDate + "'),'" + u.Nickname + "','" + u.Email + "','" + u.Password + "','" + u.City + "')", myConnection);
            myCommand1.ExecuteNonQuery();
        }

        private string EncryptPassword(string password)
        {
            byte[] pass = Encoding.UTF8.GetBytes(password);
            SHA256 encpwrd = new SHA256CryptoServiceProvider();
            return Encoding.UTF8.GetString(encpwrd.ComputeHash(pass));
        }
    }
}
