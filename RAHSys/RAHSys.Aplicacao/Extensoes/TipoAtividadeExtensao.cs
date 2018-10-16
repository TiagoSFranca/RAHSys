using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class TipoAtividadeExtensao
    {
        public static TipoAtividadeModel MapearParaDominio(this TipoAtividadeAppModel obj)
        {
            return AutoMapper.Mapper.Map<TipoAtividadeModel>(obj);
        }

        public static TipoAtividadeAppModel MapearParaAplicacao(this TipoAtividadeModel obj)
        {
            return AutoMapper.Mapper.Map<TipoAtividadeAppModel>(obj);
        }
    }
}
