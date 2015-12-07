using System.Web.Optimization;

namespace OrganizationsWebApplication
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var scriptBundle = new ScriptBundle("~/commonBundle");

            scriptBundle.Include("~/Scripts/Libraries/jquery-{version}.js");
            scriptBundle.Include("~/Scripts/Libraries/jquery.unobtrusive*");
            scriptBundle.Include("~/Scripts/Libraries/jquery.validate*");
            scriptBundle.Include("~/Scripts/UIscripts/changeView.js");

            var styleBundle = new StyleBundle("~/content/style");
            styleBundle.Include("~/Content/style.css");

            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);
            //////////////////////////////
            var angularBundle = new ScriptBundle("~/angularBundle");

            scriptBundle.Include("~/Scripts/Libraries/angular.js");
            scriptBundle.Include("~/Scripts/Libraries/angular-route.js");
       
            bundles.Add(angularBundle);
        }
    }
}