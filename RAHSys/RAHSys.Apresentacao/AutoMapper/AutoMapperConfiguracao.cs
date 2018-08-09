using AutoMapper;
using RAHSys.Aplicacao.Mapeamento;

namespace RAHSys.Apresentacao.AutoMapper
{
    public class AutoMapperConfiguracao
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile(new ConfiguracaoGeralMapeamento());

                x.AddProfile(new CameraMapeamento());

                x.AddProfile(new TipoTelhadoMapeamento());

                x.AddProfile(new TipoContatoMapeamento());

                x.AddProfile(new ContratoMapeamento());

                x.AddProfile(new EstadoMapeamento());

                x.AddProfile(new EnderecoMapeamento());

                x.AddProfile(new ContratoEnderecoMapeamento());

                x.AddProfile(new CidadeMapeamento());
            });
        }
    }
}