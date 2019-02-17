using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Mapeamento.Profiles.Interfaces;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento.Profiles
{
    public class RegistroRecorrenciaMapeamento : IProfile
    {
        public void Mapear(Profile profile)
        {
            profile.CreateMap<RegistroRecorrenciaModel, RegistroRecorrenciaAppModel>().ReverseMap();
        }
    }
}
