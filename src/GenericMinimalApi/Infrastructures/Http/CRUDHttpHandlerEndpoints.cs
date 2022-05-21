namespace GenericMinimalApi.Infrastructures.Http
{
    public static class CRUDHttpHandlerEndpoints
    {
        public static void CRUDHttpHandlerMap<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem>(this IEndpointRouteBuilder app, Action<CRUDHttpHandlerConfiguration<TEntity>>? configure = null)
            where TEntity : IEntity<TKey>
            where TListItem : IEntityDto<TKey>
            where TGetItem : IEntityDto<TKey>
            where TPostITem : IEntityDto<TKey>
            where TPutItem : IEntityDto<TKey>
        {
            var configuration = new CRUDHttpHandlerConfiguration<TEntity>();
            configure?.Invoke(configuration);

            var handler = new CRUDHttpHandler<TEntity, TKey, TListItem, TGetItem, TPostITem, TPutItem>(configuration.Entity);

            AddRoute(
                () => handler.Search(app, configuration.TextFilterFunc),
                enableCheck: configuration.EnableList,
                autorizeCheck: configuration.RequiredAutorize && configuration.RequiredAutorizeList
            );

            AddRoute(
                ()=> handler.Get(app),
                enableCheck: configuration.EnableGetById,
                autorizeCheck: configuration.RequiredAutorize && configuration.RequiredAutorizeGetById
            );

            AddRoute(
                () => handler.Post(app),
                enableCheck: configuration.EnablePost,
                autorizeCheck: configuration.RequiredAutorize && configuration.RequiredAutorizePost
            );

            AddRoute(
               () => handler.Put(app),
               enableCheck: configuration.EnablePut,
               autorizeCheck: configuration.RequiredAutorize && configuration.RequiredAutorizePut
            );

            AddRoute(
              () => handler.Delete(app),
              enableCheck: configuration.EnableDelete,
              autorizeCheck: configuration.RequiredAutorize && configuration.RequiredAutorizeDelete
            );
        }

        private static void AddRoute(Func<RouteHandlerBuilder> route, bool enableCheck, bool autorizeCheck)
        {
            if(enableCheck)
            { 
                var handler = route();
                if(autorizeCheck)
                    handler.RequireAuthorization();
            }
        }
    }
}