using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class TipoAtividadeMapeamento : Profile
    {
        public TipoAtividadeMapeamento()
        {
            CreateMap<TipoAtividadeModel, TipoAtividadeAppModel>().ReverseMap();
        }
    }
}
