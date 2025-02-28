using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HR_Medical_Records_Management_System.Configs
{
    //This class is used to filter the schema of the DateOnly type
    public class DateOnlySchemaFilter : ISchemaFilter
    {
        //this method turns the DateOnly type into a string with the format "date"
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            // verify if the type is DateOnly
            if (context.Type == typeof(DateOnly))
            {
                schema.Type = "string";
                schema.Format = "date";
                schema.Example = new OpenApiString("yyyy-MM-dd");
            }
        }
    }
}
