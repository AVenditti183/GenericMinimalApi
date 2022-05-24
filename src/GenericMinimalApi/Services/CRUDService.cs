using AutoMapper;
using AutoMapper.QueryableExtensions;
using GenericMinimalApi.Infrastructures;
using System.Linq.Expressions;
using System.Security.Claims;

namespace GenericMinimalApi.Services
{
    public class CRUDService<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem> : ICRUDService<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem>
        where TEntity : IEntity<TKey>
        where TListItem : IEntityDto<TKey>
        where TGetItem : IEntityDto<TKey>
        where TPostITem : IEntityDto<TKey>
        where TPutItem : IEntityDto<TKey>
    {
        private readonly IRepository<TEntity, TKey> repository;
        private readonly IMapper mapper;

        public CRUDService(IRepository<TEntity,TKey> repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public virtual async Task<TGetItem?> Get(TKey key, ClaimsPrincipal user)
        {
            var entity= await repository.Get(key);

            return mapper.Map<TGetItem>(entity);
        }

        public virtual async Task Create(TPostITem item, ClaimsPrincipal user)
        {
            var entity = mapper.Map<TEntity>(item);

            await repository.Create(entity);

            item.Id = entity.Id;
        }


        public virtual Task Delete(TKey key, ClaimsPrincipal user)
            => repository.Delete(key);

        
        public async Task Update(TPutItem item, TKey key, ClaimsPrincipal user)
        {
            var entity = mapper.Map<TEntity>(item);

            await repository.Update(entity);
        }


        protected virtual Expression<Func<TEntity, bool>>? Filter(string? textFilter)
            => null;

        public virtual async Task<Page<TListItem>> Search(SearchParameters parameters, Func<string, Expression<Func<TEntity, bool>>> textFilterFunc, ClaimsPrincipal user)
        {
            var entities = await repository.GetAll();

            if (!string.IsNullOrEmpty(parameters.TextFilter))
            {
                var predicate = textFilterFunc is not null 
                    ? textFilterFunc(parameters.TextFilter)
                    : Filter(parameters.TextFilter);

                if (predicate is not null)
                {
                    entities = entities.Where(predicate);
                }
            }

            var itemCount = entities.Count();
            int pageCount = (itemCount + parameters.PageSize- 1) / parameters.PageSize;
            if (parameters.Page > pageCount) parameters.Page = pageCount;
            if (parameters.Page < 1) parameters.Page = 1;

            if (!string.IsNullOrEmpty(parameters.OrderBy))
            {
                try
                {
                    if (parameters.OrderDirection == OrderDirections.Ascendent)
                    {
                        entities = entities.OrderByProperty(parameters.OrderBy);
                    }
                    else
                    {
                        entities = entities.OrderByPropertyDescending(parameters.OrderBy);
                    }
                }
                catch
                {
                    parameters.OrderBy = null;
                    parameters.OrderDirection = OrderDirections.Ascendent;
                }
            }

            var result = new Page<TListItem>()
            {
                CurrentPage = parameters.Page,
                ItemsCount = itemCount,
                Pages = pageCount,
                OrderBy = parameters.OrderBy,
                OrderDirection = parameters.OrderDirection,
                Items= entities
                .Skip((parameters.Page -1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ProjectTo<TListItem>(mapper.ConfigurationProvider)
                .ToArray()
            };

            return result;
        }
    }
}
