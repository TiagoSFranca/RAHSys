using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class ServicoAuditoriaBase<TEntity> : IServicoAuditoriaBase<TEntity> where TEntity : class
    {
        private readonly IRepositorioAuditoriaBase<TEntity> _repository;

        public ServicoAuditoriaBase(IRepositorioAuditoriaBase<TEntity> repository)
        {
            _repository = repository;
        }

        public void Adicionar(TEntity obj)
        {
            _repository.Adicionar(obj);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public TEntity ObterPorId(int id)
        {
            return _repository.ObterPorId(id);
        }
    }
}
