using System.Web;
using System.Web.Optimization;

namespace Multi_language.Client
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

            #region AdminLte Scripts

            bundles.Add(new ScriptBundle("~/bundles/adminLte").Include(
                        // FastClick
                        "~/Scripts/AdminLTE/plugins/fastclick/fastclick.min.js",
                        //  AdminLTE App
                        "~/Scripts/AdminLTE/app.js",
                        // Sparkline
                        "~/Scripts/AdminLTE/plugins/sparkline/jquery.sparkline.min.js",
                        // jvectormap
                        "~/Scripts/AdminLTE/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                        "~/Scripts/AdminLTE/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                        // daterangepicker
                        "~/Scripts/AdminLTE/plugins/daterangepicker/daterangepicker.js",
                        // datepicker
                        "~/Scripts/AdminLTE/plugins/datepicker/bootstrap-datepicker.js",
                        // iCheck
                        "~/Scripts/AdminLTE/plugins/iCheck/icheck.min.js",
                        // SlimScroll 1.3.0
                        "~/Scripts/AdminLTE/plugins/slimScroll/jquery.slimscroll.min.js",
                        // ChartJS 1.0.1
                        "~/Scripts/AdminLTE/plugins/chartjs/Chart.min.js"
                        ));

            #endregion

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            #region AdminLte Styles

            bundles.Add(new StyleBundle("~/Content/adminLte").Include(
           // Morris chart
           "~/Scripts/AdminLTE/plugins/morris/morris.css",
           // jvectormap
           "~/Scripts/AdminLTE/plugins/jvectormap/jquery-jvectormap-1.2.2.css",
           // Daterange picker
           "~/Scripts/AdminLTE/plugins/daterangepicker/daterangepicker-bs3.css",
           // Theme style
           "~/Content/AdminLTE/AdminLTE.min.css",
           // TODO: AdminLTE Skins. Choose a skin from the css/skins folder instead of downloading all of them to reduce the load.
           "~/Content/AdminLTE/skins/_all-skins.min.css"

           ));

            #endregion
        }
    }
}
