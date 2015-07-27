using System.Web.Optimization;

namespace HappyStation.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/modernizr-{version}.js",
                "~/Scripts/ckeditor/ckeditor.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/bundles/site").Include(
                "~/Content/skel-noscript.css",
                "~/Content/style.css",
                "~/Content/jquery.fancybox-buttons.css",
                "~/Content/jquery.fancybox-thumbs.css",
                "~/Content/jquery.fancybox.css",
                "~/Content/style-desktop.css"));

            bundles.Add(new ScriptBundle("~/bundles/init").Include(
                "~/Scripts/site/skel.min.js",
                "~/Scripts/site/skel-panels.min.js",
                "~/Scripts/site/init.js",
                "~/Scripts/site/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/fancybox").Include(
                "~/Scripts/jquery.fancybox.js",
                "~/Scripts/jquery.fancybox-buttons.js",
                "~/Scripts/jquery.fancybox-media.js",
                "~/Scripts/jquery.fancybox-thumbs.js",
                "~/Scripts/jquery.mousewheel-3.0.6.pack.js"));
        }
    }
}