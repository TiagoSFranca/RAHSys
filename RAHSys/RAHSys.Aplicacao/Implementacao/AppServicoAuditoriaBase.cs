using log4net;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RAHSys.Aplicacao.Implementacao
{
    public class AppServicoAuditoriaBase<TEntity> : IDisposable where TEntity : class
    {
        private readonly IServicoAuditoriaBase<TEntity> _serviceBase;

        protected readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public AppServicoAuditoriaBase(IServicoAuditoriaBase<TEntity> serviceBase)
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
