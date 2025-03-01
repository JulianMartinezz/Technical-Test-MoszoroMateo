using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HR_Medical_Records_Management_System.Configs
{
    /// <summary>
    /// Custom schema filter for handling the OpenAPI representation of the DateOnly type.
    /// This filter converts the DateOnly type to a string with the format "date" in the OpenAPI schema.
    /// </summary>
    public class DateOnlySchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Applies the custom schema filter to the OpenAPI schema for the DateOnly type.
        /// Converts the DateOnly type to a string type with the format "date" in the OpenAPI documentation.
        /// </summary>
        /// <param name="schema">The OpenApiSchema that describes the type in the OpenAPI documentation.</param>
        /// <param name="context">The SchemaFilterContext which contains the type and other context-related data.</param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(DateOnly))
            {
                schema.Type = "string";
                schema.Format = "date";
                schema.Example = new OpenApiString("yyyy-MM-dd");
            }
        }
    }
}
