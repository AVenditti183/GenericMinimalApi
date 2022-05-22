namespace IntroMinimalApi.Extensions;
public static class IEndpointRouteBuilderExtensions
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    => MapEndpoints(app, Assembly.GetCallingAssembly());

    public static void MapEndpoints(this IEndpointRouteBuilder app, Assembly assembly)
    {
        var endpointHandlerTypes = assembly.GetTypes().Where(t =>
            t.IsClass && !t.IsAbstract && !t.IsGenericType
            && t.GetConstructor(Type.EmptyTypes) != null
            && typeof(IEndpointHandler).IsAssignableFrom(t));

        foreach (var endpointHandlerType in endpointHandlerTypes)
        {
            var instantiatedType = (IEndpointHandler)Activator.CreateInstance(endpointHandlerType)!;
            instantiatedType.MapEndpoints(app);
        }
    }
}
