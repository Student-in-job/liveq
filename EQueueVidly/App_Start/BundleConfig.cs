using System.Web;
using System.Web.Optimization;

namespace EQueueVidly
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap/bootstrap.js",
                        "~/Scripts/bootstrap/bootstrap-datepicker.js",
                        "~/Scripts/bootstrap/bootstrap-datetimepicker.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootbox").Include(
                        "~/scripts/bootbox.js"));
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/scripts/datatables/jquery.datatables.js",
                        "~/scripts/datatables/datatables.bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                        "~/Scripts/moment.js"));
            bundles.Add(new ScriptBundle("~/bundles/typehead").Include(
                        "~/scripts/typehead/typeahead.bundle.js"));
            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                        "~/scripts/toastr.js"));
            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                        "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        /* "~/Scripts/jquery-{version}.js",*/
                         "~/Scripts/jquery/jquery-1.10.2.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                         "~/Scripts/jquery/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include(
                         "~/Scripts/fullcalendar.js",
                         "~/Scripts/scheduler.js",
                         "~/Scripts/jquery/jquery-cron.js"));
            bundles.Add(new ScriptBundle("~/bundles/timepicker").Include(
                         "~/Scripts/jquery.ui.timepicker.addon/jquery-ui-sliderAccess.js",
                         "~/Scripts/jquery.ui.timepicker.addon/jquery-ui-timepicker-addon.js"));
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                      "~/Scripts/knockout-{version}.js",
                      "~/Scripts/app.js"));
            bundles.Add(new ScriptBundle("~/bundles/teacher-calendar").Include(
                "~/Scripts/teacher-calendar.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/datatable").Include(
                        "~/content/datatables/css/datatables.bootstrap.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/typehead").Include(
                        "~/content/typeahead.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/toastr").Include(
                        "~/content/toastr.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/bootstrap-flatly").Include(
                        "~/Content/bootstrap/bootstrap-flatly.css",
                        "~/Content/bootstrap/font-awesome.css"
                        ));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap/bootstrap.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/timepicker").Include(
                        "~/Content/jquery-ui/jquery-ui-timepicker-addon.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                        "~/Content/datepicker.css"
                        ));

            bundles.Add(new StyleBundle("~/Content/jquery-ui").Include(
                        "~/Content/jquery-ui/jquery-ui.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/fullcalendar").Include(
                        "~/Content/fullcalendar.css",
                        "~/Content/scheduler.css",
                        "~/Content/jquery-ui/jquery-cron.css"
                        ));

            BundleTable.EnableOptimizations = true;

            
        }
    }
}
