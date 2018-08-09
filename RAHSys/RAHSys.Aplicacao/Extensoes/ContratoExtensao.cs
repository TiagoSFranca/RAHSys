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
    }
}
