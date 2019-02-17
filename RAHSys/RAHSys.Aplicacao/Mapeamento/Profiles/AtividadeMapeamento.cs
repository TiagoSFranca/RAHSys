using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Mapeamento.Profiles.Interfaces;
using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento.Profiles
{
    public class AtividadeMapeamento : IProfile
    {
        public AtividadeMapeamento()
        {
        }

        public void Mapear(Profile profile)
        {
            profile.CreateMap<AtividadeModel, AtividadeAppModel>().ReverseMap();

            profile.CreateMap<ConfiguracaoAtividadeModel, ConfiguracaoAtividadeAppModel>().ReverseMap();

            profile.CreateMap<AtividadeRecorrenciaModel, AtividadeRecorrenciaAppModel>().ReverseMap();
        }
    }
}
