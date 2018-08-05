using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Infra.Dados.Contexto;
using System;
using System.Data.Entity;
using System.Linq;

namespace RAHSys.Infra.Dados.Repositorios
{
    public class RepositorioBase<TEntity> : IDisposable, IRepositorioBase<TEntity> where TEntity : class
    {
        protected RAHSysContexto _context = new RAHSysContexto();

        public void Adicionar(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public TEntity ObterPorId(int id, bool detached = false)
        {
            var element = _context.Set<TEntity>().Find(id);
            if (detached && element != null)
                _context.Entry(element).State = EntityState.Detached;
            return element;

        }

        public void Remover(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }

        public void Atualizar(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<TEntity> Consultar()
        {
            return _context.Set<TEntity>().AsQueryable();
        }
    }
}
