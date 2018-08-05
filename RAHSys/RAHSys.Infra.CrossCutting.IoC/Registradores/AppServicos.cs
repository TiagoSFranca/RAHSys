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
        }
    }
}
