﻿using System.Web;
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
                      "~/Scripts/knockout-3.4.0.js",
                      "~/Scripts/knockout.mapping-latest.js",
                      "~/Scripts/jquery.slimscroll.js",
                      "~/Scripts/DataTables/jquery.dataTables.js",
                      "~/Scripts/DataTables/dataTables.bootstrap.js",
                      "~/Scripts/jquery.icheck.min.js",
                      "~/Scripts/select2.js",
                      "~/Scripts/pace.js",
                      "~/Scripts/plugins/sortable.min.js",
                      "~/Scripts/plugins/purify.min.js",
                      "~/Scripts/fileinput.min.js",
                      "~/Scripts/Chart.min.js",
                      "~/Scripts/MltApi/Knockout/MltApi-Ko-ChartJS.js",
                      // "~/Scripts/AddToHomeScreen/addtohomescreen.min.js",

                      "~/Scripts/MltApi/MltApi-AjaxHelpers.js",
                      "~/Scripts/MltApi/MltApi-InitPlugins.js",
                      "~/Scripts/MltApi/MltApi-LayoutHelpers.js",
                      "~/Scripts/MltApi/Settings/ChartJs-Settings.js",
                      "~/Scripts/MltApi/Settings/iCheck-Settings.js",
                      "~/Scripts/MltApi/Settings/InputFile-Settings.js",

                      "~/Scripts/MltApi/PagesHelpers/MltApi-DocMultiLanguage.js",
                      "~/Content/bootstrap-fileinput/themes/fa/theme.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      // Bootstrap 3.3.6
                      "~/Content/bootstrap.css",
                      // File input Bootstrap
                      "~/Content/bootstrap-fileinput/css/fileinput.min.css",
                      // Font Awesome
                      "~/Content/cssbundle/font-awesome.min.css",
                      // Theme style
                      "~/admin-lte/css/AdminLTE.min.css",

                      // Plugins styles
                      "~/Content/DataTables/css/dataTables.bootstrap.css",
                      "~/Content/cssbundle/select2.css",
                      "~/Content/iCheck/square/red.css",
                      /*
                       * AdminLTE Skins. We have chosen the skin-blue for this starter
                       * page. However, you can choose any other skin. Make sure you
                       * apply the skin class to the body tag so the changes take effect.
                       */
                      "~/admin-lte/css/skins/skin-red.css",
                      "~/Content/pace/themes/white/pace-theme-flash.css",
                      "~/Content/pace.css",
                      "~/Content/Custom.min.css"
                      // "~/Content/AddToHomeScreen/addtohomescreen.css"
                      ));
        }
    }
}
