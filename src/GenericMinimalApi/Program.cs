var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

public interface IService<TEntity, TListItem, TItem> : IService<TEntity, int, TListItem, TItem, TItem, TItem>
    where TEntity : IEntity<int>
    where TListItem : IEntityDto<int>
    where TItem : IEntityDto<int>
{
}

public interface IService<TEntity, TKey, TListItem, TItem> : IService<TEntity, TKey, TListItem, TItem, TItem, TItem>
    where TEntity : IEntity<TKey>
    where TListItem : IEntityDto<TKey>
    where TItem : IEntityDto<TKey>
{
}

public interface IService<TEntity, TKey, TGellAllItem, TGetItem, TPostITem, TPutItem>
    where TEntity : IEntity<TKey>
    where TGellAllItem : IEntityDto<TKey>
    where TGetItem : IEntityDto<TKey>
    where TPostITem : IEntityDto<TKey>
    where TPutItem : IEntityDto<TKey>
{
}

public interface IEntity<TKey>
{
    public TKey Id { get; set; }
}

public interface IEntityDto<TKey>
{
    public TKey Id { get; set; }
}