using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Mapeamento.Profiles.Interfaces;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento.Profiles
{
    public class DiaSemanaMapeamento : IProfile
    {
        public void Mapear(Profile profile)
        {
            profile.CreateMap<DiaSemanaModel, DiaSemanaAppModel>().ReverseMap();

            profile.CreateMap<AtividadeDiaSemanaModel, AtividadeDiaSemanaAppModel>().ReverseMap();
        }
    }
}
