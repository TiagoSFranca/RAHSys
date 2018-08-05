﻿using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Dominio.Servicos.Servicos;
using SimpleInjector;

namespace RAHSys.Infra.CrossCutting.IoC.Registradores
{
    public class Servicos
    {
        public static void Register(Container container)
        {
            container.Register<ICameraServico, CameraServico>(Lifestyle.Scoped);
            container.Register<ITipoTelhadoServico, TipoTelhadoServico>(Lifestyle.Scoped);
        }
    }
}
