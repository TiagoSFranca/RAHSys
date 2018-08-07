using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RAHSys.Apresentacao.Startup))]
namespace RAHSys.Apresentacao
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
