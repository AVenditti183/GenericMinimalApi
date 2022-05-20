namespace IntroMinimalApi.Interfaces
{
    public interface IEntityService<T>
    {
        public IEnumerable<T> GetList(string searchText);
        public T Get( Guid id);
        public Guid Add(T entity);
        public void Update(Guid id, T entity);
        public void Delete(Guid id);
    }
}
