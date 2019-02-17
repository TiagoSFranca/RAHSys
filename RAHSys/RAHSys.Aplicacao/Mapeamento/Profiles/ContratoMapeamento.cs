using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Mapeamento.Profiles.Interfaces;
using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento.Profiles
{
    public class ContratoMapeamento : IProfile
    {
        public void Mapear(Profile profile)
        {
            profile.CreateMap<ContratoModel, ContratoAppModel>().ReverseMap();
            profile.CreateMap<ContratoEnderecoModel, ContratoEnderecoAppModel>().ReverseMap();
            profile.CreateMap<AnaliseInvestimentoModel, AnaliseInvestimentoAppModel>().ReverseMap();
            profile.CreateMap<FiadorModel, FiadorAppModel>().ReverseMap();
            profile.CreateMap<FiadorEnderecoModel, FiadorEnderecoAppModel>().ReverseMap();
            profile.CreateMap<ClienteModel, ClienteAppModel>().ReverseMap();
            profile.CreateMap<DocumentoModel, DocumentoAppModel>().ReverseMap();
            profile.CreateMap<ArquivoAppModel, ArquivoModel>();
            profile.CreateMap<PagamentoModel, PagamentoAppModel>().ReverseMap();
            profile.CreateMap<ResponsavelFinanceiroModel, ResponsavelFinanceiroAppModel>().ReverseMap();
        }
    }
}
