using System.Collections.Generic;
using System.Linq;

namespace RAHSys.Dominio.Servicos.Interfaces.Repositorios
{
    public interface IRepositorioBase<TEntity> where TEntity : class
    {
        void Adicionar(TEntity obj);

        void Atualizar(TEntity obj);

        TEntity ObterPorId(int id, bool detached = false);

        void Remover(TEntity obj);

        void Dispose();

        IQueryable<TEntity> Consultar();
    }
}
