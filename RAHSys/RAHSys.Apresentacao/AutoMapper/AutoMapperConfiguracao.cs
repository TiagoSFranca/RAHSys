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
                x.AddProfile(new ConfiguracaoPerfilAplicacao());

                x.AddProfile(new CameraPerfilAplicacao());

            });
        }
    }
}