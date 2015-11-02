using System.Web.Optimization;

namespace OrganizationsWebApplication
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var scriptBundle = new ScriptBundle("~/bundles/js");

            scriptBundle.Include("~/Scripts/jquery-{version}.js");
            scriptBundle.Include("~/Scripts/jquery.unobtrusive*");
            scriptBundle.Include("~/Scripts/jquery.validate*");
            scriptBundle.Include("~/Scripts/changeView.js");
            scriptBundle.Include("~/Scripts/registrationDdl.js");
        
            var styleBundle = new StyleBundle("~/content/style");
            styleBundle.Include("~/Content/style.css");

            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);
        }
    }
}