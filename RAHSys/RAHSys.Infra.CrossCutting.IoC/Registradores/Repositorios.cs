using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Infra.Dados.Repositorios;
using SimpleInjector;

namespace RAHSys.Infra.CrossCutting.IoC.Registradores
{
    public class Repositorios
    {
        public static void Register(Container container)
        {
            container.Register<ICameraRepositorio, CameraRepositorio>(Lifestyle.Scoped);
        }
    }
}
