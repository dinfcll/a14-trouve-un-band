using System.Web;
using System.Web.Optimization;

namespace TrouveUnBand
{
    public class BundleConfig
    {
        // Pour plus d’informations sur le Bundling, accédez à l’adresse http://go.microsoft.com/fwlink/?LinkId=254725 (en anglais)
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                        "~/Content/bootstrap.min.css",
                        "~/Content/bootstrap-responsive.min.css"));
        }
    }
}
