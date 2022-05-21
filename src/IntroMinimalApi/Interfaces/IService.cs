namespace IntroMinimalApi.Interfaces;
public interface IService<TModel>
{
    public IEnumerable<TModel> GetList(string searchText);
    public TModel Get(Guid id);
    public TModel Add(TModel model);
    public void Update(Guid id, TModel model);
    public void Delete(Guid id);
}