using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class EstadoCivilExtension
    {
        public static EstadoCivilModel MapearParaDominio(this EstadoCivilAppModel obj)
        {
            return AutoMapper.Mapper.Map<EstadoCivilModel>(obj);
        }

        public static EstadoCivilAppModel MapearParaAplicacao(this EstadoCivilModel obj)
        {
            return AutoMapper.Mapper.Map<EstadoCivilAppModel>(obj);
        }
    }
}
