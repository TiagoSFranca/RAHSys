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
                "~/Content/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                "~/Content/assets/global/plugins/morris/morris.min.js",
                "~/Content/assets/global/plugins/morris/raphael-min.js",
                "~/Content/assets/global/plugins/fullcalendar/fullcalendar.min.js",
                "~/Content/assets/global/plugins/fullcalendar/lang-all.js",
                "~/Content/assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js"
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

            bundles.Add(new ScriptBundle("~/Login/js").Include(
                "~/Content/assets/pages/scripts/login.min.js"
                ));

            bundles.Add(new ScriptBundle("~/LoginPageLevel/js").Include(
                "~/Content/assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
                "~/Content/assets/global/plugins/jquery-validation/js/additional-methods.min.js"
                ));

            bundles.Add(new ScriptBundle("~/Pagamento/js").Include(
                "~/Scripts/pagamento.js"
                ));

            bundles.Add(new ScriptBundle("~/MultiSelect/js").Include(
                "~/Content/assets/global/plugins/jquery-multi-select/js/jquery.multi-select.js"
                ));

            bundles.Add(new ScriptBundle("~/Integrantes/js").Include(
                "~/Scripts/integrantes.js"
                ));

            bundles.Add(new ScriptBundle("~/Atividade/js").Include(
                "~/Scripts/atividade/atividade.js"
                ));

            bundles.Add(new ScriptBundle("~/Contrato/Atividade/js").Include(
                "~/Scripts/contrato/contratoAtividade.js"
                ));

            bundles.Add(new ScriptBundle("~/Contrato/Atividade/AdicionarEditar/js").Include(
                "~/Scripts/contrato/atividadeAdicionarEditar.js"
                ));

            bundles.Add(new ScriptBundle("~/Select2/js").Include(
                "~/Content/assets/global/plugins/select2/js/select2.full.min.js"
                ));

            bundles.Add(new ScriptBundle("~/Atividade/EvidenciasFileInput/js").Include(
                "~/Scripts/atividade/evidenciasAtividadeFileInput.js"
                ));


            bundles.Add(new ScriptBundle("~/Equipe/Atividade/js").Include(
                "~/Scripts/equipe/equipeAtividade.js"
                ));

            bundles.Add(new ScriptBundle("~/Equipe/Atividade/AdicionarEditar/js").Include(
                "~/Scripts/equipe/atividadeAdicionarEditar.js"
                ));
        }
    }
}