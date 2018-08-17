using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class UsuarioMapeamento : Profile
    {
        public UsuarioMapeamento()
        {
            CreateMap<UsuarioModel, UsuarioAppModel>().ReverseMap();
            CreateMap<UsuarioPerfilModel, UsuarioPerfilAppModel>().ReverseMap();
            CreateMap<PerfilModel, PerfilAppModel>().ReverseMap();
        }
    }
}
