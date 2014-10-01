using System.Web.Optimization;

namespace HappyStation.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/bundles/site").Include(
                "~/Content/skel-noscript.css",
                "~/Content/style.css",
                "~/Content/style-desktop.css"));

            bundles.Add(new ScriptBundle("~/bundles/init").Include(
                "~/Scripts/site/skel.min.js",
                "~/Scripts/site/skel-panels.min.js",
                "~/Scripts/site/init.js",
                "~/Scripts/site/main.js"));
        }
    }
}