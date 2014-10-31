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
private TrouveUnBandEntities1 db = new TrouveUnBandEntities1();
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
public ActionResult Register(UserValidation userModel)
{
string RC = "";
if (ModelState.IsValid)
{
if (userModel.Password == userModel.ConfirmPassword)
{
userModel = SetUserLocation(userModel);
userModel.Photo = StockPhoto();
User userBD = CreateUserFromModel(userModel);
RC = Insertcontact(userBD);
if (RC == "")
{
TempData["success"] = "L'inscription est confirmée!";
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
db.Database.Connection.Open();
userbd.Password = Encrypt(userbd.Password);
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
private string UpdateProfil(UserValidation userModel)
{
try
{
User LoggedOnUser = db.Users.FirstOrDefault(x => x.Nickname == userModel.Nickname);
if ((LoggedOnUser.Latitude == 0.0 || LoggedOnUser.Longitude == 0.0) || LoggedOnUser.Location != userModel.Location)
{
userModel = SetUserLocation(userModel);
}
LoggedOnUser = CreateUserFromModel(userModel, LoggedOnUser);
db.SaveChanges();
return "";
}
catch
{
return "Une erreur interne s'est produite. Veuillez réessayer plus tard";
}
}
private string UpdateProfil(User user)
{
try
{
User LoggedOnUser = db.Users.FirstOrDefault(x => x.Nickname == User.Identity.Name);
User MusicianQuery = db.Users.FirstOrDefault(x => x.UserId == LoggedOnUser.UserId);
if (MusicianQuery == null)
{
MusicianQuery = new User();
MusicianQuery.Description = user.Description;
MusicianQuery.UserId = LoggedOnUser.UserId;
MusicianQuery.Join_Musician_Instrument = user.Join_Musician_Instrument;
db.Users.Add(MusicianQuery);
db.SaveChanges();
}
else
{
MusicianQuery.Description = user.Description;
MusicianQuery.Join_User_Instrument.Clear();
MusicianQuery.Join_User_Instrument = user.Join_User_Instrument;
db.SaveChanges();
}
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
UserValidation LoggedOnUserValid = GetUserInfo(User.Identity.Name);
if (LoggedOnUserValid.Photo != null)
{
LoggedOnUserValid.PhotoName = "data:image/jpeg;base64," + Convert.ToBase64String(LoggedOnUserValid.Photo);
}
ViewData["UserData"] = LoggedOnUserValid;
User MusicianQuery = db.Users.FirstOrDefault(x => x.UserId == LoggedOnUserValid.UserId);
if (MusicianQuery == null)
{
MusicianQuery = new User();
}
ViewData["MusicianProfilData"] = MusicianQuery;
return View();
}
[HttpPost]
public ActionResult UserProfileModification(UserValidation userModel)
{
userModel.Nickname = User.Identity.Name;
string RC = "";
if (Request.Files[0].ContentLength == 0)
{
userModel.Photo = GetProfilePicByte(userModel.Nickname);
RC = UpdateProfil(userModel);
}
else
{
HttpPostedFileBase PostedPhoto = Request.Files[0];
try
{
Image img = Image.FromStream(PostedPhoto.InputStream, true, true);
byte[] bytephoto = imageToByteArray(img);
userModel.PhotoName = PostedPhoto.FileName;
userModel.Photo = bytephoto;
}
catch
{
userModel.Photo = StockPhoto();
}
RC = UpdateProfil(userModel);
}
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
Join_User_Instrument InstrumentsMusician = new Join_User_Instrument();
InstrumentsMusician.InstrumentId = Convert.ToInt32(InstrumentArray[i]);
InstrumentsMusician.Skills = Convert.ToInt32(SkillArray[i]);
InstrumentsMusician.UserId = user.UserId;
user.Join_User_Instrument.Add(InstrumentsMusician);
}
RC = UpdateProfil(user);
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
private UserValidation GetUserInfo(string Nickname)
{
UserValidation LoggedOnUser = new UserValidation();
try
{
LoggedOnUser = new UserValidation(db.Users.FirstOrDefault(x => x.Nickname == Nickname));
}
catch (DbEntityValidationException ex)
{
Console.WriteLine(ex.Message);
}
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
var PicQuery = (from User in db.Users
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
UserValidation LoggedOnUser = GetUserInfo(User.Identity.Name);
string PhotoName = "data:image/jpeg;base64," + Convert.ToBase64String(LoggedOnUser.Photo);
return PhotoName;
}
public UserValidation SetUserLocation(UserValidation user)
{
var client = new HttpClient();
client.BaseAddress = new Uri("https://maps.googleapis.com");
var response = client.GetAsync("/maps/api/geocode/json?address=" + user.Location + ",Canada,+CA&key=AIzaSyAzPU-uqEi7U9Ry15EgLAVZ03_4rbms8Ds").Result;
if (response.IsSuccessStatusCode)
{
string responseBody = response.Content.ReadAsStringAsync().Result;
var location = new JavaScriptSerializer().Deserialize<LocationModels>(responseBody);
user.Latitude = location.results[location.results.Count - 1].geometry.location.lat;
user.Longitude = location.results[location.results.Count - 1].geometry.location.lng;
return user;
}
user.Latitude = 0.0;
user.Longitude = 0.0;
return user;
}
public string GetDistance(double LatitudeP1,double LongitudeP1, double LatitudeP2, double LongitudeP2)
{
double R = 6378.137; // Earth’s mean radius in kilometer
var lat = ToRadians(LatitudeP2 - LatitudeP1);
var lng = ToRadians(LongitudeP2 - LongitudeP1);
var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
Math.Cos(ToRadians(LatitudeP1)) * Math.Cos(ToRadians(LatitudeP2)) *
Math.Sin(lng / 2) * Math.Sin(lng / 2);
var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
int d= (int)(R * h2);
return d.ToString() + " kilomètres";
}
private static double ToRadians(double val)
{
return (Math.PI / 180) * val;
}
private User CreateUserFromModel(UserValidation UserValid)
{
User user = new User();
user.BirthDate = UserValid.BirthDate;
user.Email = UserValid.Email;
user.FirstName = UserValid.FirstName;
user.Gender = UserValid.Gender;
user.LastName = UserValid.LastName;
user.Latitude = UserValid.Latitude;
user.Location = UserValid.Location;
user.Longitude = UserValid.Longitude;
user.Nickname = UserValid.Nickname;
user.Password = UserValid.Password;
user.Photo = UserValid.Photo;
user.UserId = UserValid.UserId;
return user;
}
private User CreateUserFromModel(UserValidation UserValid, User user)
{
user.BirthDate = UserValid.BirthDate;
user.Email = UserValid.Email;
user.FirstName = UserValid.FirstName;
user.Gender = UserValid.Gender;
user.LastName = UserValid.LastName;
user.Latitude = UserValid.Latitude;
user.Location = UserValid.Location;
user.Longitude = UserValid.Longitude;
user.Nickname = UserValid.Nickname;
user.Photo = UserValid.Photo;
user.UserId = UserValid.UserId;
return user;
}
}
}