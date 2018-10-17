using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class AtividadeMapeamento : Profile
    {
        public AtividadeMapeamento()
        {
            CreateMap<AtividadeModel, AtividadeAppModel>().ReverseMap();
        }
    }
}
