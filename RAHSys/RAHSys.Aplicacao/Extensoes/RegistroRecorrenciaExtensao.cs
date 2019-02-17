using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class RegistroRecorrenciaExtensao
    {
        public static RegistroRecorrenciaModel MapearParaDominio(this RegistroRecorrenciaAppModel obj)
        {
            return AutoMapper.Mapper.Map<RegistroRecorrenciaModel>(obj);
        }

        public static RegistroRecorrenciaAppModel MapearParaAplicacao(this RegistroRecorrenciaModel obj)
        {
            return AutoMapper.Mapper.Map<RegistroRecorrenciaAppModel>(obj);
        }
    }
}
