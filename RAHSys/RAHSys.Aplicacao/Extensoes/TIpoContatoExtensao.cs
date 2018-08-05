using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class TipoContatoExtension
    {
        public static TipoContatoModel MapearParaDominio(this TipoContatoAppModel obj)
        {
            return AutoMapper.Mapper.Map<TipoContatoModel>(obj);
        }

        public static TipoContatoAppModel MapearParaAplicacao(this TipoContatoModel obj)
        {
            return AutoMapper.Mapper.Map<TipoContatoAppModel>(obj);
        }
    }
}
