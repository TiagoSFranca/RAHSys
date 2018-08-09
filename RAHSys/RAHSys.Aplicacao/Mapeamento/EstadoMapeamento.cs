using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class EstadoMapeamento : Profile
    {
        public EstadoMapeamento()
        {
            CreateMap<EstadoModel, EstadoAppModel>().ReverseMap();
        }
    }
}
