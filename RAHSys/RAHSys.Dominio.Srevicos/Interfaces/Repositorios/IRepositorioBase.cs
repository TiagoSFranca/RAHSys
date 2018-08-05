using System.Linq;

namespace RAHSys.Dominio.Servicos.Interfaces.Repositorios
{
    public interface IRepositorioBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);

        void Update(TEntity obj);

        TEntity GetById(int id, bool detached = false);

        void Remove(TEntity obj);

        void Dispose();

        IQueryable<TEntity> Query();
    }
}
