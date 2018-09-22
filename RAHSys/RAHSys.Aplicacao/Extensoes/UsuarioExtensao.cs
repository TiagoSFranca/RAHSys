using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class UsuarioExtensao
    {
        public static UsuarioModel MapearParaDominio(this UsuarioAppModel obj)
        {
            return AutoMapper.Mapper.Map<UsuarioModel>(obj);
        }

        public static UsuarioAppModel MapearParaAplicacao(this UsuarioModel obj)
        {
            return AutoMapper.Mapper.Map<UsuarioAppModel>(obj);
        }
    }
}
