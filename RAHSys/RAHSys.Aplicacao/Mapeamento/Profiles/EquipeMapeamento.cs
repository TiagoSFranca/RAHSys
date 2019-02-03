using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Mapeamento.Profiles.Interfaces;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento.Profiles
{
    public class EquipeMapeamento : IProfile
    {
        public void Mapear(Profile profile)
        {
            profile.CreateMap<EquipeModel, EquipeAppModel>().ReverseMap();
            profile.CreateMap<EquipeUsuarioModel, EquipeUsuarioAppModel>().ReverseMap();
        }
    }
}
