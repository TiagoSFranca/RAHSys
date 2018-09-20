using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class ContratoMapeamento : Profile
    {
        public ContratoMapeamento()
        {
            CreateMap<ContratoModel, ContratoAppModel>().ReverseMap();
            CreateMap<ContratoEnderecoModel, ContratoEnderecoAppModel>().ReverseMap();
            CreateMap<AnaliseInvestimentoModel, AnaliseInvestimentoAppModel>().ReverseMap();
            CreateMap<FiadorModel, FiadorAppModel>().ReverseMap();
            CreateMap<FiadorEnderecoModel, FiadorEnderecoAppModel>().ReverseMap();
            CreateMap<ClienteModel, ClienteAppModel>().ReverseMap();
            CreateMap<DocumentoModel, DocumentoAppModel>().ReverseMap();
            CreateMap<ArquivoAppModel, ArquivoModel>();
            CreateMap<PagamentoModel, PagamentoAppModel>().ReverseMap();
        }
    }
}
