using RAHSys.Infra.CrossCutting.IoC.Registradores;
using RAHSys.Infra.Dados.Contexto;
using SimpleInjector;

namespace RAHSys.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            container.Register<RAHSysContexto>(Lifestyle.Scoped);

            AppServicos.Register(container);

            Servicos.Register(container);

            Repositorios.Register(container);

        }
    }
}
