using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class EstadoExtension
    {
        public static EstadoModel MapearParaDominio(this EstadoAppModel obj)
        {
            return AutoMapper.Mapper.Map<EstadoModel>(obj);
        }

        public static EstadoAppModel MapearParaAplicacao(this EstadoModel obj)
        {
            return AutoMapper.Mapper.Map<EstadoAppModel>(obj);
        }
    }
}
