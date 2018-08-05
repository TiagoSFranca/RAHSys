using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class TipoTelhadoPerfilAplicacao : Profile
    {
        public TipoTelhadoPerfilAplicacao()
        {
            CreateMap<TipoTelhadoModel, TipoTelhadoAppModel>().ReverseMap();
        }
    }
}
