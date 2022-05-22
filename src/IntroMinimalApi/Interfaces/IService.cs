namespace IntroMinimalApi.Interfaces;
public interface IService<TModel>
{
    public IEnumerable<TModel> GetList(GetListParameters parameters);
    public TModel Get(Guid id);
    public TModel Add(TModel model);
    public void Update(Guid id, TModel model);
    public void Delete(Guid id);
    public byte[] GetPhoto(Guid id);
    public void AddPhoto(Guid id, FormFileContent photo);
    public void DeletePhoto(Guid id);
}