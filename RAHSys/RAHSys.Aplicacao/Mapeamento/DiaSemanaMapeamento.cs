using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class DiaSemanaMapeamento : Profile
    {
        public DiaSemanaMapeamento()
        {
            CreateMap<DiaSemanaModel, DiaSemanaAppModel>().ReverseMap();

            CreateMap<AtividadeDiaSemanaModel, AtividadeDiaSemanaAppModel>().ReverseMap();
        }
    }
}
