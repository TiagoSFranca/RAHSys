namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IServicoBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);

        void Update(TEntity obj);

        TEntity GetById(int id);

        void Remove(TEntity obj);

        void Dispose();

    }
}
