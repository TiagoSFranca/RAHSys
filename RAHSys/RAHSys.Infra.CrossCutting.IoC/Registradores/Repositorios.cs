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
            container.Register<IAuditoriaRepositorio, AuditoriaRepositorio>(Lifestyle.Scoped);
            container.Register<IContratoRepositorio, ContratoRepositorio>(Lifestyle.Scoped);
            container.Register<IEstadoRepositorio, EstadoRepositorio>(Lifestyle.Scoped);
            container.Register<ICidadeRepositorio, CidadeRepositorio>(Lifestyle.Scoped);
            container.Register<IEnderecoRepositorio, EnderecoRepositorio>(Lifestyle.Scoped);
            container.Register<IContratoEnderecoRepositorio, ContratoEnderecoRepositorio>(Lifestyle.Scoped);
            container.Register<IAnaliseInvestimentoRepositorio, AnaliseInvestimentoRepositorio>(Lifestyle.Scoped);
            container.Register<IEstadoCivilRepositorio, EstadoCivilRepositorio>(Lifestyle.Scoped);
            container.Register<IClienteRepositorio, ClienteRepositorio>(Lifestyle.Scoped);
            container.Register<IDocumentoRepositorio, DocumentoRepositorio>(Lifestyle.Scoped);
        }
    }
}
