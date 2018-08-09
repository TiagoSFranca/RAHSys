using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class ContratoEnderecoMapeamento : Profile
    {
        public ContratoEnderecoMapeamento()
        {
            CreateMap<ContratoEnderecoModel, ContratoEnderecoAppModel>().ReverseMap();
        }
    }
}
