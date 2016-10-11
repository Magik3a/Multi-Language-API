using System.Web;
using System.Web.Optimization;

namespace Multi_Language.MVCClient
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/theme").Include(
                      "~/admin-lte/js/app.min.js",
                      "~/Scripts/fastclick.js",
                      "~/Scripts/jquery.slimscroll.js",
                      "~/Scripts/DataTables/jquery.dataTables.js",
                      "~/Scripts/DataTables/dataTables.bootstrap.js",
                      "~/Scripts/jquery.icheck.min.js",
                      "~/Scripts/select2.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      // Bootstrap 3.3.6
                      "~/Content/bootstrap.css",
                      // Font Awesome
                      "~/Content/css/font-awesome.min.css",
                      // Theme style
                      "~/admin-lte/css/AdminLTE.min.css",

                      // Plugins styles
                      "~/Content/DataTables/css/dataTables.bootstrap.css",
                      "~/Content/css/select2.css",
                      "~/Content/iCheck/square/blue.css",
                      /*
                       * AdminLTE Skins. We have chosen the skin-blue for this starter
                       * page. However, you can choose any other skin. Make sure you
                       * apply the skin class to the body tag so the changes take effect.
                       */
                      "~/admin-lte/css/skins/skin-red.css"));
        }
    }
}
