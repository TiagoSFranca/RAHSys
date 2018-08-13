using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class EstadoCivilMapeamento : Profile
    {
        public EstadoCivilMapeamento()
        {
            CreateMap<EstadoCivilModel, EstadoCivilAppModel>().ReverseMap();
        }
    }
}
