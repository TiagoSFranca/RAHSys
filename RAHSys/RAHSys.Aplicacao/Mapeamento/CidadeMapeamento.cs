using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class CidadeMapeamento : Profile
    {
        public CidadeMapeamento()
        {
            CreateMap<CidadeModel, CidadeAppModel>().ReverseMap();
        }
    }
}
