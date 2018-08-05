using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class TipoContatoMapeamento : Profile
    {
        public TipoContatoMapeamento()
        {
            CreateMap<TipoContatoModel, TipoContatoAppModel>().ReverseMap();
        }
    }
}
