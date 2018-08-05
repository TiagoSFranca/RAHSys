namespace RAHSys.Aplicacao.Interfaces
{
    public interface IAppServicoBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);

        void Update(TEntity obj);

        TEntity GetById(int id);

        void Remove(int id);
    }
}
