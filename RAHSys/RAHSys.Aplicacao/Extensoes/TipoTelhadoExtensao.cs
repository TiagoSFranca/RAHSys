using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class TipoTelhadoExtensao
    {
        public static TipoTelhadoModel MapearParaDominio(this TipoTelhadoAppModel obj)
        {
            return AutoMapper.Mapper.Map<TipoTelhadoModel>(obj);
        }

        public static TipoTelhadoAppModel MapearParaAplicacao(this TipoTelhadoModel obj)
        {
            return AutoMapper.Mapper.Map<TipoTelhadoAppModel>(obj);
        }
    }
}
