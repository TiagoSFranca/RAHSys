using AutoMapper;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class ConfiguracaoGeralMapeamento : Profile
    {
        public ConfiguracaoGeralMapeamento()
        {
            CreateMap<string, string>().ConvertUsing(str => (str ?? "").Trim());
        }
    }
}
