using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IntroMinimalApi.Binding
{
    public class GetListOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var routeNameMetadata = (IRouteNameMetadata) context.ApiDescription.ActionDescriptor.EndpointMetadata.SingleOrDefault(m => m is IRouteNameMetadata);

            if (routeNameMetadata != null && 
                routeNameMetadata.RouteName.StartsWith("Get") && 
                routeNameMetadata.RouteName.EndsWith("List"))
            {
                if (operation.Parameters == null)
                { 
                    operation.Parameters = new List<OpenApiParameter>();
                }

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "searchtext",
                    In = ParameterLocation.Query,
                    Required = false
                });
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "pagenumber",
                    In = ParameterLocation.Query,
                    Required = false
                });
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "pagesize",
                    In = ParameterLocation.Query,
                    Required = false
                });
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "orderby",
                    In = ParameterLocation.Query,
                    Required = false
                });
            }
        }
    }
}
