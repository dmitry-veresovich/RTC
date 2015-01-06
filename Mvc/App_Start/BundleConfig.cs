using System.Web.Optimization;

namespace Rtc.Mvc
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            RegisterPageBundles(bundles);

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-{version}"));


            //bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
            //    "~/Scripts/jquery.signalR-{version}.js",
            //    "~/signalr/hubs"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-responsive.css",
                "~/Content/Site.css"));
        }

        private static void RegisterPageBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/search").Include(
                "~/Scripts/search.js"));

            bundles.Add(new ScriptBundle("~/bundles/friendAction").Include(
                "~/Scripts/friendAction.js"));

            bundles.Add(new ScriptBundle("~/bundles/chat").Include(
                "~/Scripts/chat.js"));

        }
    }
}