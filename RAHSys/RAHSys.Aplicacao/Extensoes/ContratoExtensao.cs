using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class ContratoExtension
    {
        public static ContratoModel MapearParaDominio(this ContratoAppModel obj)
        {
            return AutoMapper.Mapper.Map<ContratoModel>(obj);
        }

        public static ContratoAppModel MapearParaAplicacao(this ContratoModel obj)
        {
            return AutoMapper.Mapper.Map<ContratoAppModel>(obj);
        }

        public static AnaliseInvestimentoModel MapearParaDominio(this AnaliseInvestimentoAppModel obj)
        {
            return AutoMapper.Mapper.Map<AnaliseInvestimentoModel>(obj);
        }

        public static AnaliseInvestimentoAppModel MapearParaAplicacao(this AnaliseInvestimentoModel obj)
        {
            return AutoMapper.Mapper.Map<AnaliseInvestimentoAppModel>(obj);
        }

        public static ClienteModel MapearParaDominio(this ClienteAppModel obj)
        {
            return AutoMapper.Mapper.Map<ClienteModel>(obj);
        }

        public static ClienteAppModel MapearParaAplicacao(this ClienteModel obj)
        {
            return AutoMapper.Mapper.Map<ClienteAppModel>(obj);
        }

        public static ArquivoModel MapearParaDominio(this ArquivoAppModel obj)
        {
            return AutoMapper.Mapper.Map<ArquivoModel>(obj);
        }
    }
}
