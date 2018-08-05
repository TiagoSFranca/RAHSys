using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class CameraMapeamento : Profile
    {
        public CameraMapeamento()
        {
            CreateMap<CameraModel, CameraAppModel>().ReverseMap();
        }
    }
}
