using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento
{
    public class AuditoriaMapeamento : Profile
    {
        public AuditoriaMapeamento()
        {
            CreateMap<AuditoriaModel, AuditoriaAppModel>().ReverseMap();
        }
    }
}
