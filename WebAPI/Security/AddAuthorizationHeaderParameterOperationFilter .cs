using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPI.Security;

public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var controllerType = context.ApiDescription.ActionDescriptor.RouteValues["controller"];
        if (!string.IsNullOrWhiteSpace(controllerType) && !controllerType.Equals("Auth", StringComparison.OrdinalIgnoreCase))
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = "Bearer token",
                Required = false
            });
        }
    }
}
