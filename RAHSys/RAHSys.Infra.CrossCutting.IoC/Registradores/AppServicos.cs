using RAHSys.Aplicacao.Implementacao;
using RAHSys.Aplicacao.Interfaces;
using SimpleInjector;

namespace RAHSys.Infra.CrossCutting.IoC.Registradores
{
    public class AppServicos
    {
        public static void Register(Container container)
        {
            container.Register<ICameraAppServico, CameraAppServico>(Lifestyle.Scoped);
            container.Register<ITipoTelhadoAppServico, TipoTelhadoAppServico>(Lifestyle.Scoped);
            container.Register<ITipoContatoAppServico, TipoContatoAppServico>(Lifestyle.Scoped);
            container.Register<IContratoAppServico, ContratoAppServico>(Lifestyle.Scoped);
            container.Register<IEstadoAppServico, EstadoAppServico>(Lifestyle.Scoped);
        }
    }
}
