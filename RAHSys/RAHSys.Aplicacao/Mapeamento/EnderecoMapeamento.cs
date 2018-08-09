using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class EnderecoMapeamento : Profile
    {
        public EnderecoMapeamento()
        {
            CreateMap<EnderecoModel, EnderecoAppModel>().ReverseMap();
        }
    }
}
