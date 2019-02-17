using AutoMapper;
using RAHSys.Aplicacao.Mapeamento.Profiles;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class MapeamentoGeral : Profile
    {
        public MapeamentoGeral()
        {
            new AtividadeMapeamento().Mapear(this);
            new AuditoriaMapeamento().Mapear(this);
            new CameraMapeamento().Mapear(this);
            new CidadeMapeamento().Mapear(this);
            new ConfiguracaoGeralMapeamento().Mapear(this);
            new ContratoMapeamento().Mapear(this);
            new DiaSemanaMapeamento().Mapear(this);
            new EnderecoMapeamento().Mapear(this);
            new EquipeMapeamento().Mapear(this);
            new EstadoCivilMapeamento().Mapear(this);
            new EstadoMapeamento().Mapear(this);
            new RegistroRecorrenciaMapeamento().Mapear(this);
            new TipoAtividadeMapeamento().Mapear(this);
            new TipoContatoMapeamento().Mapear(this);
            new TipoRecorrenciaMapeamento().Mapear(this);
            new TipoTelhadoMapeamento().Mapear(this);
            new UsuarioMapeamento().Mapear(this);
            new EvidenciaMapeamento().Mapear(this);
        }
    }
}
