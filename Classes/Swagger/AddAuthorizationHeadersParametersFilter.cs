using System;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.Swagger;
using System.Web.Http.Description;

namespace ventaVehiculosAPI.Classes.Swagger
{
    public class AddAuthorizationHeadersParametersFilter : IOperationFilter
    {
        /// <summary>
        /// Implement view Swagger
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
            {
                operation.parameters = new List<Swashbuckle.Swagger.Parameter>();
            }

            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                @in = "header",
                description = "Access token",
                required = false,
                type = "string"
            });
        }
    }
}