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
using System.Drawing.Drawing2D;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Web.Script.Serialization;
using System.Net.Http;

namespace TrouveUnBand.Controllers
{
    public class UsersController : Controller
    {
        private TrouveUnBandEntities db = new TrouveUnBandEntities();

        public ActionResult test()
        {
            return View();
        }

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

        public ActionResult ViewProfile(string type, int Id)
        {
            switch(type.ToUpper())
            {
                case "MUSICIEN" : //view model
                    Musician musician = db.Musicians.FirstOrDefault(x => x.MusicianId == Id);
                    MusicianProfileViewModel ViewProfile = CreateMusicianProfileView(musician);
                    return View("MusicianProfile", ViewProfile);
                
                case "BAND":
                    break;
                
                case "EVENT":
                    break;
                
                case "PROMOTER":
                    break;
               
                default:
                    break;
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            string RC = "";
            if (ModelState.IsValid)
            {
                if (user.Password == user.ConfirmPassword)
                {
                    user = SetUserLocation(user);
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
                var ValidUserQuery = (from User in db.Users
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
                    db.Users.Add(user);
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

        private string UpdateProfil(User user)
        {
            try
            {
                User LoggedOnUser = db.Users.FirstOrDefault(x => x.Nickname == user.Nickname);

                if ((LoggedOnUser.Latitude == 0.0 || LoggedOnUser.Longitude == 0.0) || LoggedOnUser.Location != user.Location)
                {
                    user = SetUserLocation(user);
                }

                LoggedOnUser.LastName = user.LastName;
                LoggedOnUser.Location = user.Location;
                LoggedOnUser.BirthDate = user.BirthDate;
                LoggedOnUser.Email = user.Email;
                LoggedOnUser.FirstName = user.FirstName;
                LoggedOnUser.Gender = user.Gender;
                LoggedOnUser.Latitude = user.Latitude;
                LoggedOnUser.Longitude = user.Longitude;

                db.SaveChanges();

                return "";
            }
            catch
            {
                return "Une erreur interne s'est produite. Veuillez réessayer plus tard";
            }
        }

        private string UpdateProfil(Musician musician)
        {
            try
            {
                User LoggedOnUser = db.Users.FirstOrDefault(x => x.Nickname == User.Identity.Name);

                Musician MusicianQuery = db.Musicians.FirstOrDefault(x => x.UserId == LoggedOnUser.UserId);

                if (MusicianQuery == null)
                {
                    MusicianQuery = new Musician();
                    MusicianQuery.Description = musician.Description;
                    MusicianQuery.UserId = LoggedOnUser.UserId;
                    MusicianQuery.Join_Musician_Instrument = musician.Join_Musician_Instrument;
                    db.Musicians.Add(MusicianQuery);
                    db.SaveChanges();
                }
                else
                {
                    MusicianQuery.Description = musician.Description;
                    MusicianQuery.Join_Musician_Instrument.Clear();
                    MusicianQuery.Join_Musician_Instrument = musician.Join_Musician_Instrument;
                    MusicianQuery.User.ConfirmPassword = MusicianQuery.User.Password;
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

            User LoggedOnUser = GetUserInfo(User.Identity.Name);
            if (LoggedOnUser.Photo != null)
            {
                LoggedOnUser.ProfilePicture.stringProfilePicture = "data:image/jpeg;base64," + Convert.ToBase64String(LoggedOnUser.Photo);
            }

            User UserQuery = db.Users.FirstOrDefault(x => x.UserId == LoggedOnUser.UserId);
            ViewData["UserData"] = UserQuery;

            Musician MusicianQuery = db.Musicians.FirstOrDefault(x => x.UserId == LoggedOnUser.UserId);
            if (MusicianQuery == null)
            {
                MusicianQuery = new Musician();
            }
            ViewData["MusicianProfilData"] = MusicianQuery;
            return View();
        }

        [HttpPost]
        public ActionResult UserProfileModification(User user)
        {
                user.Nickname = User.Identity.Name;
                string RC = "";

                RC = UpdateProfil(user);

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
        public ActionResult MusicianProfileModification(Musician musician)
        {
            string InstrumentList = Request["InstrumentList"];
            string[] InstrumentArray = InstrumentList.Split(',');
            string RC;

            if (AllUnique(InstrumentArray))
            {
                string SkillList = Request["SkillsList"];
                string[] SkillArray = SkillList.Split(',');
                string DescriptionMusician = Request["TextArea"];

                musician.Description = DescriptionMusician;
                for (int i = 0; i < InstrumentArray.Length; i++)
                {
                    int CurrentInstrumentID = Convert.ToInt32(InstrumentArray[i]);
                    Instrument instrument = db.Instruments.FirstOrDefault(x => x.InstrumentId == CurrentInstrumentID);
                    Join_Musician_Instrument InstrumentsMusician = new Join_Musician_Instrument();

                    InstrumentsMusician.InstrumentId = instrument.InstrumentId;
                    InstrumentsMusician.Skills = Convert.ToInt32(SkillArray[i]);
                    InstrumentsMusician.MusicianId = musician.MusicianId;

                    musician.Join_Musician_Instrument.Add(InstrumentsMusician);
                }

                RC = UpdateProfil(musician);
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

        private User GetUserInfo(string Nickname)
        {
            User LoggedOnUser = new User();
            try
            {
                LoggedOnUser = db.Users.FirstOrDefault(x => x.Nickname == Nickname);
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
                                byteProfilePicture = User.Photo
                            }).FirstOrDefault();
            return PicQuery.byteProfilePicture;
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

        public User SetUserLocation(User user)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("https://maps.googleapis.com");

            var response = client.GetAsync("/maps/api/geocode/json?address=" 
                                            + user.Location 
                                            + ",Canada,+CA&key=AIzaSyAzPU-uqEi7U9Ry15EgLAVZ03_4rbms8Ds"
                                            ).Result;

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
                int d=  (int)(R * h2);
                return d.ToString() + " kilomètres";
        }

        private static double ToRadians(double val)
        {
            return (Math.PI / 180) * val;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CropImage(User UserPicture)
        {
            User LoggedOnUser = db.Users.FirstOrDefault(x => x.Nickname == User.Identity.Name);
            if (Request.Files[0].ContentLength == 0)
            {
                TempData["TempDataError"] = "Une erreur s'est produite lors de l'ouverture du fichier. Veuillez réessayer. ";
            }
            else
            {
                HttpPostedFileBase PostedPhoto = Request.Files[0];
                try
                {
                    string extension = Path.GetExtension(PostedPhoto.FileName).ToLower();

                    if (extension != ".jpe" && extension != ".jpg" && extension != ".jpeg" && extension != ".gif" && extension != ".png" &&
                        extension != ".pns" && extension != ".bmp" && extension != ".ico" && extension != ".psd" && extension != ".pdd")
                    {
                        TempData["TempDataError"] = "Le type du fichier n'est pas valide. Assurez-vous que le fichier soit bien une image. ";
                        return RedirectToAction("ProfileModification");
                    }

                    Image image = Image.FromStream(PostedPhoto.InputStream, true, true);

                    if (image.Height < 172 || image.Width < 250 || image.Height > 413 || image.Width > 600)
                    {
                        image = ResizeOriginalImage(image, 172, 250, 413, 600);
                    }

                    byte[] croppedImage = CropImage(image, 
                                          UserPicture.ProfilePicture.PicX,
                                          UserPicture.ProfilePicture.PicY,
                                          UserPicture.ProfilePicture.PicWidth,
                                          UserPicture.ProfilePicture.PicHeight);

                    LoggedOnUser.Photo = croppedImage;
                    db.SaveChanges();
                }
                catch
                {
                    TempData["TempDataError"] = "Une erreur inattendue s'est produite. Veuillez réessayer plus tard. ";
                }
            }
            TempData["success"] = "La photo de profil a été modifiée avec succès.";
            return RedirectToAction("ProfileModification", "Users");
        }

        private static byte[] CropImage(Image image, int x, int y, int width, int height)
        {
            Rectangle CropRect = new Rectangle(x, y, width, height);
            Bitmap btmOriginalImage = new Bitmap(image);
            Bitmap btmNewImage = new Bitmap(CropRect.Width, CropRect.Height);
            byte[] CroppedImage;

            using (Graphics g = Graphics.FromImage(btmNewImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.DrawImage(btmOriginalImage, new Rectangle(0, 0, btmNewImage.Width, btmNewImage.Height),CropRect,GraphicsUnit.Pixel);
            }

            CroppedImage = GetBitmapBytes(btmNewImage);
            return CroppedImage;
        }

        private static byte[] GetBitmapBytes(Bitmap source)
        {
            ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders()[4];
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

            using (MemoryStream tmpStream = new MemoryStream())
            {
                source.Save(tmpStream, codec, parameters);

                byte[] result = new byte[tmpStream.Length];
                tmpStream.Seek(0, SeekOrigin.Begin);
                tmpStream.Read(result, 0, (int)tmpStream.Length);

                return result;
            }
        }

        private Image ResizeOriginalImage(Image ImageToResize, int MinHeight, int MinWidth, int MaxHeight, int MaxWidth)
        {
            Bitmap btmNewImage;

            int OriginalHeight = ImageToResize.Height;
            int OriginalWidth = ImageToResize.Width;
            int NewHeight;
            int NewWidth;

            if (OriginalHeight < MinHeight || OriginalWidth < MinWidth)
            {
                if (OriginalHeight < MinHeight)
                {
                    NewHeight = MinHeight;
                }
                else
                {
                    NewHeight = ImageToResize.Height;
                }

                if (OriginalWidth < MinWidth)
                {
                    NewWidth = MinWidth;
                }
                else
                {
                    NewWidth = ImageToResize.Width;
                }
            }
            else
            {
                if (OriginalHeight > MaxHeight)
                {
                    NewHeight = MaxHeight;
                }
                else
                {
                    NewHeight = ImageToResize.Height;
                }

                if (OriginalWidth > MaxWidth)
                {
                    NewWidth = MaxWidth;
                }
                else
                {
                    NewWidth = ImageToResize.Width;
                }
            }

            btmNewImage = new Bitmap(NewWidth, NewHeight);
            using (Graphics gr = Graphics.FromImage(btmNewImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(ImageToResize, new Rectangle(0, 0, NewWidth, NewHeight));
            }

            return (Image)btmNewImage;

        }

        private MusicianProfileViewModel CreateMusicianProfileView(Musician musician)
        {
            MusicianProfileViewModel ProfileView = new MusicianProfileViewModel();

            User user = db.Users.FirstOrDefault(x => x.UserId == musician.UserId);

            ICollection<Join_Musician_Instrument> ListOfInstruments = musician.Join_Musician_Instrument.OrderByDescending(x => (x.Skills)).ToList();
            List<string> InstrumentName = new List<string>();
            List<string> SkillList = new List<string> { "Aucun", "Débutant", "Initié", "Intermédiaire", "Avancé", "Légendaire" };

            ProfileView.InstrumentName = new List<string>();
            ProfileView.InstrumentSkill = new List<string>();
            for (int i = 0; i < ListOfInstruments.Count; i++)
            {
                ProfileView.InstrumentName.Add(ListOfInstruments.ElementAt(i).Instrument.Name);
                ProfileView.InstrumentSkill.Add(SkillList[ListOfInstruments.ElementAt(i).Skills]);
            }

            ProfileView.Description = musician.Description;
            ProfileView.Name = user.FirstName + " " + user.LastName;
            ProfileView.Location = user.Location;
            ProfileView.ProfilePicture = new Photo();
            ProfileView.ProfilePicture.stringProfilePicture = "data:image/jpeg;base64," + Convert.ToBase64String(user.Photo);

            return ProfileView;
        }
    }
}

