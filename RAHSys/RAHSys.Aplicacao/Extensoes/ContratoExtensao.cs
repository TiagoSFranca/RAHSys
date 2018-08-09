using RAHSys.Aplicacao.AppModels;
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
    }
}
