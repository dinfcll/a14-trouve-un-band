using System.Linq;
using System.Web.Mvc;
using TrouveUnBand.Models;

namespace TrouveUnBand.Controllers
{
    public class SocialMediaController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult test()
        {
            return View();
        }

        public static Band GetBand(int i)
        {
            TrouveUnBandEntities statdb = new TrouveUnBandEntities();
            var query = statdb.Bands.FirstOrDefault(x => x.Band_ID == i);
            return query;
        }

        public static Event GetEvent(int i)
        {
            TrouveUnBandEntities statdb = new TrouveUnBandEntities();
            var query = statdb.Events.FirstOrDefault(x => x.Event_ID == i);
            return query;
        }
    }
}
