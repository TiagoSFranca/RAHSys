using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class TipoRecorrenciaExtensao
    {
        public static TipoRecorrenciaModel MapearParaDominio(this TipoRecorrenciaAppModel obj)
        {
            return AutoMapper.Mapper.Map<TipoRecorrenciaModel>(obj);
        }

        public static TipoRecorrenciaAppModel MapearParaAplicacao(this TipoRecorrenciaModel obj)
        {
            return AutoMapper.Mapper.Map<TipoRecorrenciaAppModel>(obj);
        }
    }
}
