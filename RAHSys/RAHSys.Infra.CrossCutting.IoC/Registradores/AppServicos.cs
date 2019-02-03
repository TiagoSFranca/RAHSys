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
            container.Register<IAuditoriaAppServico, AuditoriaAppServico>(Lifestyle.Scoped);
            container.Register<IContratoAppServico, ContratoAppServico>(Lifestyle.Scoped);
            container.Register<IEstadoAppServico, EstadoAppServico>(Lifestyle.Scoped);
            container.Register<ICidadeAppServico, CidadeAppServico>(Lifestyle.Scoped);
            container.Register<IEstadoCivilAppServico, EstadoCivilAppServico>(Lifestyle.Scoped);
            container.Register<IDocumentoAppServico, DocumentoAppServico>(Lifestyle.Scoped);
            container.Register<IPagamentoAppServico, PagamentoAppServico>(Lifestyle.Scoped);
            container.Register<IUsuarioAppServico, UsuarioAppServico>(Lifestyle.Scoped);
            container.Register<IEquipeAppServico, EquipeAppServico>(Lifestyle.Scoped);
            container.Register<ITipoAtividadeAppServico, TipoAtividadeAppServico>(Lifestyle.Scoped);
            container.Register<IAtividadeAppServico, AtividadeAppServico>(Lifestyle.Scoped);
            container.Register<ITipoRecorrenciaAppServico, TipoRecorrenciaAppServico>(Lifestyle.Scoped);
            container.Register<IDiaSemanaAppServico, DiaSemanaAppServico>(Lifestyle.Scoped);
            container.Register<IRegistroRecorrenciaAppServico, RegistroRecorrenciaAppServico>(Lifestyle.Scoped);
        }
    }
}
