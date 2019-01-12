using System.Linq;

namespace RAHSys.Dominio.Servicos.Interfaces.Repositorios
{
    public interface IRepositorioAuditoriaBase<TEntity> where TEntity : class
    {
        void Adicionar(TEntity obj);

        TEntity ObterPorId(int id, bool detached = false);

        void Dispose();

        IQueryable<TEntity> Consultar();    
    }
}
