using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class EquipeExtensao
    {
        public static EquipeModel MapearParaDominio(this EquipeAppModel obj)
        {
            return AutoMapper.Mapper.Map<EquipeModel>(obj);
        }

        public static EquipeAppModel MapearParaAplicacao(this EquipeModel obj)
        {
            return AutoMapper.Mapper.Map<EquipeAppModel>(obj);
        }
    }
}
