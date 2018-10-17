using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Dominio.Servicos.Servicos;
using SimpleInjector;

namespace RAHSys.Infra.CrossCutting.IoC.Registradores
{
    public class Servicos
    {
        public static void Register(Container container)
        {
            container.Register<ICameraServico, CameraServico>(Lifestyle.Scoped);
            container.Register<ITipoTelhadoServico, TipoTelhadoServico>(Lifestyle.Scoped);
            container.Register<ITipoContatoServico, TipoContatoServico>(Lifestyle.Scoped);
            container.Register<IAuditoriaServico, AuditoriaServico>(Lifestyle.Scoped);
            container.Register<IContratoServico, ContratoServico>(Lifestyle.Scoped);
            container.Register<IEstadoServico, EstadoServico>(Lifestyle.Scoped);
            container.Register<ICidadeServico, CidadeServico>(Lifestyle.Scoped);
            container.Register<IEstadoCivilServico, EstadoCivilServico>(Lifestyle.Scoped);
            container.Register<IDocumentoServico, DocumentoServico>(Lifestyle.Scoped);
            container.Register<IPagamentoServico, PagamentoServico>(Lifestyle.Scoped);
            container.Register<IUsuarioServico, UsuarioServico>(Lifestyle.Scoped);
            container.Register<IEquipeServico, EquipeServico>(Lifestyle.Scoped);
            container.Register<ITipoAtividadeServico, TipoAtividadeServico>(Lifestyle.Scoped);
            container.Register<IAtividadeServico, AtividadeServico>(Lifestyle.Scoped);
        }
    }
}
