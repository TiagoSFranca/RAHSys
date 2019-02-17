using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class EvidenciaExtensao
    {
        public static EvidenciaModel MapearParaDominio(this EvidenciaAppModel obj)
        {
            return AutoMapper.Mapper.Map<EvidenciaModel>(obj);
        }

        public static EvidenciaAppModel MapearParaAplicacao(this EvidenciaModel obj)
        {
            return AutoMapper.Mapper.Map<EvidenciaAppModel>(obj);
        }
    }
}
