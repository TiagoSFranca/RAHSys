using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class CameraPerfilAplicacao : Profile
    {
        public CameraPerfilAplicacao()
        {
            CreateMap<CameraModel, CameraAppModel>().ReverseMap();
        }
    }
}
