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
            container.Register<ITipoTelhadoRepositorio, TipoTelhadoRepositorio>(Lifestyle.Scoped);
            container.Register<ITipoContatoRepositorio, TipoContatoRepositorio>(Lifestyle.Scoped);
            container.Register<IContratoRepositorio, ContratoRepositorio>(Lifestyle.Scoped);
            container.Register<IEstadoRepositorio, EstadoRepositorio>(Lifestyle.Scoped);
            container.Register<ICidadeRepositorio, CidadeRepositorio>(Lifestyle.Scoped);
        }
    }
}
