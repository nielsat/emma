using System.Web;
using System.Web.Optimization;

namespace MailPusher
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            
            bundles.Add(new ScriptBundle("~/bundles/gentelella").Include(
                        "~/Scripts/gentelella/custom.js",
                        "~/Scripts/DataTables/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-multiselect.js",
                      "~/Scripts/bootstrap3-typeahead*",
                      "~/Scripts/bootstrap-datepicker*",
                      "~/Scripts/locales/bootstrap-datepicker*"));

            bundles.Add(new ScriptBundle("~/bundles/mailPusher").Include(
                      "~/Scripts/common/*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-multiselect.css",
                      "~/Content/datepicker/bootstrap-datepicker.css"));

            bundles.Add(new StyleBundle("~/Content/css/gentelella").Include(
                      "~/Content/font-awesome.css",
                      "~/Content/gentelella/custom.css"));
        }
    }
}
