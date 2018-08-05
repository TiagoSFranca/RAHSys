using AutoMapper;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class ConfiguracaoPerfilAplicacao : Profile
    {
        public ConfiguracaoPerfilAplicacao()
        {
            CreateMap<string, string>().ConvertUsing(str => (str ?? "").Trim());
        }
    }
}
