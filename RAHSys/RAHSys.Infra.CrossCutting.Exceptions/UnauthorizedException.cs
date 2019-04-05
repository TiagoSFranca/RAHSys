using System;

namespace RAHSys.Infra.CrossCutting.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
            : base("Acesso não autorizado.")
        {
        }
    }
}
