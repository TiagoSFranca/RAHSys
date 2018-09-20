using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class PagamentoExtensao
    {
        public static PagamentoModel MapearParaDominio(this PagamentoAppModel obj)
        {
            return AutoMapper.Mapper.Map<PagamentoModel>(obj);
        }

        public static PagamentoAppModel MapearParaAplicacao(this PagamentoModel obj)
        {
            return AutoMapper.Mapper.Map<PagamentoAppModel>(obj);
        }
    }
}
