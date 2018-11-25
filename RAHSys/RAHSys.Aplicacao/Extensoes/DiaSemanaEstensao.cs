using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class DiaSemanaExtensao
    {
        public static DiaSemanaModel MapearParaDominio(this DiaSemanaAppModel obj)
        {
            return AutoMapper.Mapper.Map<DiaSemanaModel>(obj);
        }

        public static DiaSemanaAppModel MapearParaAplicacao(this DiaSemanaModel obj)
        {
            return AutoMapper.Mapper.Map<DiaSemanaAppModel>(obj);
        }
        public static AtividadeDiaSemanaModel MapearParaDominio(this AtividadeDiaSemanaAppModel obj)
        {
            return AutoMapper.Mapper.Map<AtividadeDiaSemanaModel>(obj);
        }

        public static AtividadeDiaSemanaAppModel MapearParaAplicacao(this AtividadeDiaSemanaModel obj)
        {
            return AutoMapper.Mapper.Map<AtividadeDiaSemanaAppModel>(obj);
        }
    }
}
