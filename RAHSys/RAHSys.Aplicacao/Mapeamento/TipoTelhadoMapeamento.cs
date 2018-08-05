using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class TipoTelhadoMapeamento : Profile
    {
        public TipoTelhadoMapeamento()
        {
            CreateMap<TipoTelhadoModel, TipoTelhadoAppModel>().ReverseMap();
        }
    }
}
