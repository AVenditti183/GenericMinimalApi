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
        public string Entity { get; private set; }


        public CRUDHttpHandler(string? entity)
        {
            Entity = string.IsNullOrEmpty(entity) ? typeof(TEntity).Name : entity;
        }

        public RouteHandlerBuilder Get(IEndpointRouteBuilder app)
            => app.MapGet($"/api/{Entity}/{{id}}",
                    async (
                            TKey id,
                            ICRUDService<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem> service,
                            HttpContext context
                        )
                        =>
                    {
                        var entity = await service.Get(id, context.User);
                        return entity is null
                            ? Results.NotFound()
                            : Results.Ok(entity);
                    })
                .Produces<TGetItem>()
                .WithGroupName(Entity);

        public RouteHandlerBuilder Search(IEndpointRouteBuilder app, Func<string, Expression<Func<TEntity, bool>>> textFilterFunc)
        => app.MapGet($"/api/{Entity}",
                async (
                    string? textFilter,
                    string? orderBy,
                    OrderDirections orderDirection,
                    int page,
                    int pageSize,
                    ICRUDService<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem> service,
                    HttpContext context
                    )
                    => Results.Ok(await service.Search(new SearchParameters(textFilter, orderBy, orderDirection, page, pageSize), textFilterFunc, context.User))
            )
            .Produces<Page<TListItem>>()
            .WithGroupName(Entity);

        public RouteHandlerBuilder Post(IEndpointRouteBuilder app)
        => app.MapPost($"/api/{Entity}",
                async (
                    TPostITem item,
                    ICRUDService<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem> service,
                    HttpContext context
                ) =>
                {
                    var (isValid, errors) = ValidationModel.Validate(item);
                    if (!isValid)
                        return Results.ValidationProblem(errors);
                    try
                    {
                        await service.Create(item, context.User);
                        return Results.NoContent();
                    }
                    catch (ArgumentException e)
                    {
                        return Results.ValidationProblem(e.Map());
                    }
                }
            )
            .ProducesValidationProblem()
            .Accepts<TPostITem>("application/json")
            .WithGroupName(Entity);

        public RouteHandlerBuilder Put(IEndpointRouteBuilder app)
        => app.MapPut($"/api/{Entity}/{{id}}",
                async (
                    TKey id,
                    TPutItem item,
                    ICRUDService<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem> service,
                    HttpContext context
                ) =>
                {
                    var (isValid, errors) = ValidationModel.Validate(item);
                    if (!isValid)
                        return Results.ValidationProblem(errors);


                    var entity = await service.Get(id, context.User);
                    if (entity is null)
                        return Results.NotFound();
                    try
                    {
                        await service.Update(item, id, context.User);
                        return Results.NoContent();
                    }
                    catch (ArgumentException e)
                    {
                        return Results.ValidationProblem(e.Map());
                    }
                }
            )
            .ProducesValidationProblem()
            .Accepts<TPutItem>("application/json")
            .WithGroupName(Entity);

        public RouteHandlerBuilder Delete(IEndpointRouteBuilder app)
        => app.MapDelete($"/api/{Entity}/{{id}}",
                async (
                    TKey id,
                    ICRUDService<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem> service,
                    HttpContext context
                ) =>
                {
                    var entity = await service.Get(id, context.User);
                    if (entity is null)
                        return Results.NotFound();
                    try
                    {
                        await service.Delete(id, context.User);
                        return Results.NoContent();
                    }
                    catch (ArgumentException e)
                    {
                        return Results.ValidationProblem(e.Map());
                    }
                }
            )
            .WithGroupName(Entity);
    }
}