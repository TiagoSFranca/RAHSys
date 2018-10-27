using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class TipoRecorrenciaMapeamento : Profile
    {
        public TipoRecorrenciaMapeamento()
        {
            CreateMap<TipoRecorrenciaModel, TipoRecorrenciaAppModel>().ReverseMap();
        }
    }
}
