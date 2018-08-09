using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class CidadeExtension
    {
        public static CidadeModel MapearParaDominio(this CidadeAppModel obj)
        {
            return AutoMapper.Mapper.Map<CidadeModel>(obj);
        }

        public static CidadeAppModel MapearParaAplicacao(this CidadeModel obj)
        {
            return AutoMapper.Mapper.Map<CidadeAppModel>(obj);
        }
    }
}
