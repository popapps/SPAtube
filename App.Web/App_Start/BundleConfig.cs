using System.Web;
using System.Web.Optimization;

namespace App.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            // .debug.js, -vsdoc.js and .intellisense.js files 
            // are in BundleTable.Bundles.IgnoreList by default.
            // Clear out the list and add back the ones we want to ignore.
            // Don't add back .debug.js.
            bundles.IgnoreList.Clear();
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*intellisense.js");

            bundles.Add(new ScriptBundle("~/bundles/jquery",
                "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.2.min.js")
                .Include("~/Scripts/lib/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/lib")
                .Include(
                    "~/Scripts/lib/json2.js", // IE7 needs this
                    "~/Scripts/lib/bootstrap.js",
                    "~/Scripts/lib/knockout-{version}.js",
                    "~/Scripts/lib/knockout.mapping-latest.js",
                    "~/Scripts/lib/knockout.validation.js",
                    "~/Scripts/lib/activity-indicator.js",
                    "~/Scripts/lib/moment.js",
                    "~/Scripts/lib/amplify.js",
                    "~/Scripts/lib/sammy-{version}.js",
                    "~/Scripts/lib/toastr.js",
                    "~/Scripts/lib/underscore.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/app")
                .IncludeDirectory("~/Scripts/app", "*.js"));



            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/lib/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include(
                "~/Content/less/bootstrap.css",
                "~/Content/site.css",
                "~/Content/less/responsive.css",
                "~/Content/toastr.css",
                "~/Content/toastr-responsive.css"
                ));

        }
    }
}