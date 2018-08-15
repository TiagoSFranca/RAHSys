using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using System.Web;

namespace RAHSys.Apresentacao.AutoMapper
{
    public class ArquivoMapeamento : Profile
    {
        public ArquivoMapeamento()
        {
            CreateMap<HttpPostedFileBase, ArquivoAppModel>();
        }
    }
}