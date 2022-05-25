using System.Linq.Expressions;

namespace GenericMinimalApi.Infrastructures.Http
{
    public class CRUDHttpHandler<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem>
        where TEntity : IEntity<TKey>
        where TListItem : IEntityDto<TKey>
        where TGetItem : IEntityDto<TKey>
        where TPostITem : IEntityDto<TKey>
        where TPutItem : IEntityDto<TKey>
    {
        private readonly string entity;

        public CRUDHttpHandler(string? entity)
        {
            this.entity = string.IsNullOrEmpty(entity) ? typeof(TEntity).Name : entity;
        }

        public RouteHandlerBuilder Get(IEndpointRouteBuilder app)
            => app.MapGet($"/api/{entity}/{{id}}",
                    async (
                            TKey id,
                            ICRUDService<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem> service
                        )
                        =>
                    {
                        var entity = await service.Get(id);
                        return entity is null
                            ? Results.NotFound()
                            : Results.Ok(entity);
                    })
                .Produces<TGetItem>()
                .Produces(StatusCodes.Status404NotFound)
                .WithGroupName(entity);

        public RouteHandlerBuilder Search(IEndpointRouteBuilder app, Func<string, Expression<Func<TEntity, bool>>> textFilterFunc)
        => app.MapGet($"/api/{entity}",
                async (
                    string? textFilter,
                    string? orderBy,
                    OrderDirections orderDirection,
                    int page,
                    int pageSize,
                    ICRUDService<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem> service
                    )
                    => Results.Ok(await service.Search(new SearchParameters(textFilter, orderBy, orderDirection, page, pageSize), textFilterFunc))
            )
            .Produces<Page<TListItem>>()
            .WithGroupName(entity);

        public RouteHandlerBuilder Post(IEndpointRouteBuilder app)
        => app.MapPost($"/api/{entity}",
                async (
                    TPostITem item,
                    ICRUDService<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem> service
                ) =>
                {
                    var (isValid, errors) = ValidationModel.Validate(item);
                    if (!isValid)
                        return Results.ValidationProblem(errors);
                        
                    await service.Create(item);

                    return Results.Created($"/api/{entity}/{item.Id}",item);
                }
            )
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status201Created)
            .Accepts<TPostITem>("application/json")
            .WithGroupName(entity);

        public RouteHandlerBuilder Put(IEndpointRouteBuilder app)
        => app.MapPut($"/api/{entity}/{{id}}",
                async (
                    TKey id,
                    TPutItem item,
                    ICRUDService<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem> service
                ) =>
                {
                    var (isValid, errors) = ValidationModel.Validate(item);
                    if (!isValid)
                        return Results.ValidationProblem(errors);


                    var entity = await service.Get(id);
                    if (entity is null)
                        return Results.NotFound();

                    await service.Update(item, id);
                    return Results.NoContent();
                }
            )
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Accepts<TPutItem>("application/json")
            .WithGroupName(entity);

        public RouteHandlerBuilder Delete(IEndpointRouteBuilder app)
        => app.MapDelete($"/api/{entity}/{{id}}",
                async (
                    TKey id,
                    ICRUDService<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem> service
                ) =>
                {
                    var entity = await service.Get(id);
                    if (entity is null)
                        return Results.NotFound();

                    await service.Delete(id);
                    return Results.NoContent();
                }
            )
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithGroupName(entity);
    }
}