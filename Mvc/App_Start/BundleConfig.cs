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

            bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
                        "~/Scripts/jquery.signalR-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            RegisterAppScripts(bundles);

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-responsive.css",
                "~/Content/Site.css"));
        }

        private static void RegisterAppScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                "~/Scripts/app/layout.js"));

            bundles.Add(new ScriptBundle("~/bundles/search").Include(
                "~/Scripts/app/search.js"));

            bundles.Add(new ScriptBundle("~/bundles/friendAction").Include(
                "~/Scripts/app/friendAction.js"));

            bundles.Add(new ScriptBundle("~/bundles/chat").Include(
                "~/Scripts/app/chat.js"));

            bundles.Add(new ScriptBundle("~/bundles/manage").Include(
                "~/Scripts/app/manage.js"));

            bundles.Add(new ScriptBundle("~/bundles/account").Include(
                "~/Scripts/app/account.js"));

        }
    }
}