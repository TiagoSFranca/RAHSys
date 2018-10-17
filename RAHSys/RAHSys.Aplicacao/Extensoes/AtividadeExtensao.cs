using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class AtividadeExtensao
    {
        public static AtividadeModel MapearParaDominio(this AtividadeAppModel obj)
        {
            return AutoMapper.Mapper.Map<AtividadeModel>(obj);
        }

        public static AtividadeAppModel MapearParaAplicacao(this AtividadeModel obj)
        {
            return AutoMapper.Mapper.Map<AtividadeAppModel>(obj);
        }
    }
}
