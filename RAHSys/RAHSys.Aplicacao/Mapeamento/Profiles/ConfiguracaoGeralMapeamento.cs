using AutoMapper;
using RAHSys.Aplicacao.Mapeamento.Profiles.Interfaces;

namespace RAHSys.Aplicacao.Mapeamento.Profiles
{
    public class ConfiguracaoGeralMapeamento : IProfile
    {
        public void Mapear(Profile profile)
        {
            profile.CreateMap<string, string>().ConvertUsing(str => (str ?? "").Trim());
        }
    }
}
