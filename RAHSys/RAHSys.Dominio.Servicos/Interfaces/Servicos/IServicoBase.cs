namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IServicoBase<TEntity> where TEntity : class
    {
        void Adicionar(TEntity obj);

        void Atualizar(TEntity obj);

        TEntity ObterPorId(int id);

        void Remover(TEntity obj);

        void Dispose();

    }
}
