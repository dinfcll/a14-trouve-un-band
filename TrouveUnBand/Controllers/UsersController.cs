﻿using System;
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
            myConnection.ConnectionString = "Data Source=localhost\\sqlexpress;Initial Catalog=tempdb;Integrated Security=True";
            try
            {
                myConnection.Open();
                String query = String.Format("INSERT INTO Users(FirstName, LastName, BirthDate, Nickname, Email, Password, City) Values ('{0}','{1}',convert(datetime,'{2}'),'{3}','{4}','{5}','{6}')", u.FirstName, u.LastName, u.BirthDate, u.Nickname, u.Email, EncryptPassword(u.Password), u.City);
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

        private string EncryptPassword(string password)
        {
            byte[] pass = Encoding.UTF8.GetBytes(password);
            MD5 encpwrd = new MD5CryptoServiceProvider();
            return Encoding.UTF8.GetString(encpwrd.ComputeHash(pass));
        }
    }
}