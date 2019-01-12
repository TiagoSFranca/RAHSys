using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades;
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
        public static ConfiguracaoAtividadeModel MapearParaDominio(this ConfiguracaoAtividadeAppModel obj)
        {
            return AutoMapper.Mapper.Map<ConfiguracaoAtividadeModel>(obj);
        }

        public static ConfiguracaoAtividadeAppModel MapearParaAplicacao(this ConfiguracaoAtividadeModel obj)
        {
            return AutoMapper.Mapper.Map<ConfiguracaoAtividadeAppModel>(obj);
        }

        public static AtividadeRecorrenciaAppModel MapearParaAplicacao(this AtividadeRecorrenciaModel obj)
        {
            return AutoMapper.Mapper.Map<AtividadeRecorrenciaAppModel>(obj);
        }
    }
}
