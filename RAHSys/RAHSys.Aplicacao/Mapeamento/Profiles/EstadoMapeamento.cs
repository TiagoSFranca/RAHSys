﻿using AutoMapper;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Mapeamento.Profiles.Interfaces;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Aplicacao.Mapeamento.Profiles
{
    public class EstadoMapeamento : IProfile
    {
        public void Mapear(Profile profile)
        {
            profile.CreateMap<EstadoModel, EstadoAppModel>().ReverseMap();
        }
    }
}
