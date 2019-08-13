using System.Web;
using System.Web.Optimization;

namespace GarageProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                "~/Scripts/toastr.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                       "~/Scripts/jquery.min.js",
                      "~/Scripts/bootstrap.bundle.min.js",
                      "~/Scripts/jquery.easing.min.js",
                      "~/Scripts/jqBootstrapValidation.js",
                      "~/Scripts/contact_me.js",
                      "~/Scripts/agency.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/agency.min.css",
                        "~/Content/all.min.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/brands.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css",
                      "~/Content/validation.css",
                       "~/Content/solid.css"));
        }
    }
}
