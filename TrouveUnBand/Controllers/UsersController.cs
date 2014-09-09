using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrouveUnBand.Models;
using System.Data.SqlClient;

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
                insertcontact(u);
                return RedirectToAction("Index", "Home");
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
            SqlCommand myCommand1 = new SqlCommand("INSERT INTO Users(FirstName, LastName, BirthDate, Nickname, Email, Password) Values ('"+u.FirstName+"','"+u.LastName+"',convert(datetime,'"+u.BirthDate+"'),'"+u.Nickname+"','"+u.Email+"','"+u.Password+"')", myConnection);
            myCommand1.ExecuteNonQuery();
        }
    }
}
