namespace GenericMinimalApi.Infrastructures
{
    public interface IRepository<TEntity, TKey>
    {
        Task<IQueryable<TEntity>> GetAll();
        Task<TEntity?> Get(TKey key);
        Task Create(TEntity item);
        Task Update(TEntity item);
        Task Delete(TKey key);
    }
}