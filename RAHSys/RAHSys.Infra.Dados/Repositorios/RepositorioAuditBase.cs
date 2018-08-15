using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Infra.Dados.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAHSys.Infra.Dados.Repositorios
{
    public class RepositorioAuditBase<TEntity> : IDisposable, IRepositorioAuditoriaBase<TEntity> where TEntity : class
    {
        protected RAHSysAuditContexto _context = new RAHSysAuditContexto();

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

        public IQueryable<TEntity> Consultar()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

    }
}
