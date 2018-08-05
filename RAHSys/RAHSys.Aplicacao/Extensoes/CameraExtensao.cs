using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Extensoes
{
    internal static class CameraExtension
    {
        public static CameraModel MapearParaDominio(this CameraAppModel obj)
        {
            return AutoMapper.Mapper.Map<CameraModel>(obj);
        }

        public static CameraAppModel MapearParaAplicacao(this CameraModel obj)
        {
            return AutoMapper.Mapper.Map<CameraAppModel>(obj);
        }
    }
}
