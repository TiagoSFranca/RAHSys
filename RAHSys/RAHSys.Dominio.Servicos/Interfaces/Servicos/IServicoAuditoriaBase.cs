using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IServicoAuditoriaBase<TEntity> where TEntity : class
    {
        void Adicionar(TEntity obj);

        TEntity ObterPorId(int id);

        void Dispose();
    }
}
