using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Mapeamento.Profiles.Interfaces;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento.Profiles
{
    public class UsuarioMapeamento : IProfile
    {
        public void Mapear(Profile profile)
        {
            profile.CreateMap<UsuarioModel, UsuarioAppModel>().ReverseMap();
            profile.CreateMap<UsuarioPerfilModel, UsuarioPerfilAppModel>().ReverseMap();
            profile.CreateMap<PerfilModel, PerfilAppModel>().ReverseMap();
        }
    }
}
