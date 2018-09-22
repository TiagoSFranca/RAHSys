using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class EquipeMapeamento : Profile
    {
        public EquipeMapeamento()
        {
            CreateMap<EquipeModel, EquipeAppModel>().ReverseMap();
            CreateMap<EquipeUsuarioModel, EquipeUsuarioAppModel>().ReverseMap();
        }
    }
}
