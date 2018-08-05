using RAHSys.Apresentacao.App_Start.Bundles;
using System.Web.Optimization;

namespace RAHSys.Apresentacao
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            ScriptBundles.RegisterScripts(bundles);
            StyleBundles.RegisterStyles(bundles);
        }

    }
}
