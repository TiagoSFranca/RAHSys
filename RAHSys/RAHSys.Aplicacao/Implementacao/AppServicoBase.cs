using log4net;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;
using System.Reflection;

namespace RAHSys.Aplicacao.Implementacao
{
    public class AppServicoBase<TEntity> : IDisposable where TEntity : class
    {
        private readonly IServicoBase<TEntity> _serviceBase;

        protected readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public AppServicoBase(IServicoBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public void Dispose()
        {
            _serviceBase.Dispose();
        }

        protected void LogExceptions(CustomBaseException exceptionThrown)
        {
            LogicalThreadContext.Properties["codError"] = exceptionThrown.CodExcecao;
            logger.Error(exceptionThrown.Message, exceptionThrown);
        }

    }
}
