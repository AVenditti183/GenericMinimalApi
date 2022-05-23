using System.Linq.Expressions;
using System.Security.Claims;

namespace GenericMinimalApi.Infrastructures
{

    public interface ICRUDService<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem>
    where TEntity : IEntity<TKey>
    where TListItem : IEntityDto<TKey>
    where TGetItem : IEntityDto<TKey>
    where TPostITem : IEntityDto<TKey>
    where TPutItem : IEntityDto<TKey>
    {
        Task<Page<TListItem>> Search(SearchParameters parameters, Func<string, Expression<Func<TEntity, bool>>> textFilterFunc, ClaimsPrincipal user);
        Task<TGetItem?> Get(TKey key, ClaimsPrincipal user);
        Task Create(TPostITem item, ClaimsPrincipal user);
        Task Update(TPutItem item,TKey key, ClaimsPrincipal user);
        Task Delete(TKey key, ClaimsPrincipal user);
    }
}