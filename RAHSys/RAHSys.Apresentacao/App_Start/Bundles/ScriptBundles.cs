using System.Web.Optimization;

namespace RAHSys.Apresentacao.App_Start.Bundles
{
    public class ScriptBundles
    {
        public static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/CorePlugins/js").Include(
                "~/Content/assets/global/plugins/jquery.min.js",
                "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                "~/Content/assets/global/plugins/js.cookie.min.js",
                "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Content/assets/global/plugins/jquery.blockui.min.js",
                "~/Content/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                "~/Scripts/toastr.min.js"
                ));

            bundles.Add(new ScriptBundle("~/PageLevelPlugins/js").Include(
                "~/Content/assets/global/plugins/moment.min.js",
                "~/Content/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js",
                "~/Content/assets/global/plugins/morris/morris.min.js",
                "~/Content/assets/global/plugins/morris/raphael-min.js",
                "~/Content/assets/global/plugins/counterup/jquery.waypoints.min.js",
                "~/Content/assets/global/plugins/counterup/jquery.counterup.min.js",
                "~/Content/assets/global/plugins/fullcalendar/fullcalendar.min.js",
                "~/Content/assets/global/plugins/flot/jquery.flot.min.js",
                "~/Content/assets/global/plugins/flot/jquery.flot.resize.min.js",
                "~/Content/assets/global/plugins/flot/jquery.flot.categories.min.js",
                "~/Content/assets/global/plugins/jquery-easypiechart/jquery.easypiechart.min.js",
                "~/Content/assets/global/plugins/jquery.sparkline.min.js",
                "~/Content/assets/global/plugins/jqvmap/jqvmap/jquery.vmap.js",
                "~/Content/assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.russia.js",
                "~/Content/assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.world.js",
                "~/Content/assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.europe.js",
                "~/Content/assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.germany.js",
                "~/Content/assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.usa.js",
                "~/Content/assets/global/plugins/jqvmap/jqvmap/data/jquery.vmap.sampledata.js"
                ));

            bundles.Add(new ScriptBundle("~/Global/js").Include(
                "~/Content/assets/global/scripts/app.min.js"
                ));

            bundles.Add(new ScriptBundle("~/PageLevel/js").Include(
                "~/Content/assets/pages/scripts/dashboard.min.js"
                ));

            bundles.Add(new ScriptBundle("~/Theme/js").Include(
                "~/Content/assets/layouts/layout3/scripts/layout.min.js",
                "~/Content/assets/layouts/layout3/scripts/demo.min.js",
                "~/Content/assets/layouts/global/scripts/quick-sidebar.min.js",
                "~/Content/assets/layouts/global/scripts/quick-nav.min.js"
                ));

            bundles.Add(new ScriptBundle("~/Ordenacao/js").Include(
                "~/Scripts/ordenacao.js"
                ));

            bundles.Add(new ScriptBundle("~/Modal/js").Include(
                "~/Scripts/modal.js"
                ));

            bundles.Add(new ScriptBundle("~/BuscarCidades/js").Include(
                "~/Scripts/buscarCidades.js"
                ));

            bundles.Add(new ScriptBundle("~/FichaCliente/js").Include(
                "~/Scripts/fichaCliente.js"
                ));

            bundles.Add(new ScriptBundle("~/FileInput/js").Include(
                "~/Content/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js"
                ));
        }
    }
}