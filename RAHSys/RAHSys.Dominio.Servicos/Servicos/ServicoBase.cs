using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using System;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class ServicoBase<TEntity> : IServicoBase<TEntity> where TEntity : class
    {
        private readonly IRepositorioBase<TEntity> _repository;

        protected readonly string RootPath = AppDomain.CurrentDomain.BaseDirectory;

        public ServicoBase(IRepositorioBase<TEntity> repository)
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

        public void Remover(TEntity obj)
        {
            _repository.Remover(obj);
        }

        public void Atualizar(TEntity obj)
        {
            _repository.Atualizar(obj);
        }
    }
}
