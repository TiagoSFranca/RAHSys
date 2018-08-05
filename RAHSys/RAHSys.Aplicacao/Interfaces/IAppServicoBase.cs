namespace RAHSys.Aplicacao.Interfaces
{
    public interface IAppServicoBase<TEntity> where TEntity : class
    {
        void Adicionar(TEntity obj);

        void Atualizar(TEntity obj);

        TEntity ObterPorId(int id);

        void Remover(int id);
    }
}
