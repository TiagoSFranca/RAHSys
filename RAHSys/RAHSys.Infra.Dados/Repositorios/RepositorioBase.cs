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

        public void Add(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public TEntity GetById(int id, bool detached = false)
        {
            var element = _context.Set<TEntity>().Find(id);
            if (detached && element != null)
                _context.Entry(element).State = EntityState.Detached;
            return element;

        }

        public void Remove(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>().AsQueryable();
        }
    }
}
