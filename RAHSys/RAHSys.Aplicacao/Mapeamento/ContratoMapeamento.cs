using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class ContratoMapeamento : Profile
    {
        public ContratoMapeamento()
        {
            CreateMap<ContratoModel, ContratoAppModel>().ReverseMap();
        }
    }
}
