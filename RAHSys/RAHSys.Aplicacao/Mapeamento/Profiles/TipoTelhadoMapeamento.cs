using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Mapeamento.Profiles.Interfaces;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento.Profiles
{
    public class TipoTelhadoMapeamento : IProfile
    {
        public void Mapear(Profile profile)
        {
            profile.CreateMap<TipoTelhadoModel, TipoTelhadoAppModel>().ReverseMap();
        }
    }
}
