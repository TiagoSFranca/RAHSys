using System.Web.Optimization;

namespace RAHSys.Apresentacao.App_Start.Bundles
{
    public class StyleBundles
    {
        public static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Mandatory/css").Include(
                "~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                "~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                "~/Content/toastr.min.css",
                "~/Content/site.css"
                ));

            bundles.Add(new StyleBundle("~/PageLevelPlugins/css").Include(
                "~/Content/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css",
                "~/Content/assets/global/plugins/morris/morris.css",
                "~/Content/assets/global/plugins/fullcalendar/fullcalendar.min.css",
                "~/Content/assets/global/plugins/jqvmap/jqvmap/jqvmap.css"
                ));

            bundles.Add(new StyleBundle("~/Global/css").Include(
                "~/Content/assets/global/css/components-md.min.css",
                "~/Content/assets/global/css/plugins-md.min.css"
                ));

            bundles.Add(new StyleBundle("~/Theme/css").Include(
                "~/Content/assets/layouts/layout3/css/layout.min.css",
                "~/Content/assets/layouts/layout3/css/themes/default.min.css",
                "~/Content/assets/layouts/layout3/css/custom.min.css"
                ));

            bundles.Add(new StyleBundle("~/FileInput/css").Include(
                "~/Content/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css"
                ));
        }
    }
}