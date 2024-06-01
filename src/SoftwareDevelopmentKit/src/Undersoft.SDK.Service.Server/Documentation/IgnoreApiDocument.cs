using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace Undersoft.SDK.Service.Server.Documentation
{
    public class IgnoreApiDocument : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var apiDescription in context.ApiDescriptions)
            {
                var ignore = apiDescription.CustomAttributes().Any(c => c.GetType() == typeof(IgnoreApiAttribute));
                apiDescription.TryGetMethodInfo(out MethodInfo info);
                if (ignore || info.GetCustomAttributes<IgnoreApiAttribute>().Distinct().Any())
                {
                    string kepath = apiDescription.RelativePath;
                    var remRoutes = swaggerDoc.Paths
                                        .Where(x => x.Key.ToLower()
                                        .Contains(kepath.ToString()
                                        .ToLower())).ToArray();

                    var a = remRoutes.ForEach(x => swaggerDoc.Paths.Remove(x.Key)).ToList();
                }
            }
        }
    }

    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        //private readonly ServiceApiOptions _apiOptions;

        public AuthorizeCheckOperationFilter()
        {
            //_apiOptions = apiOptions;
        }
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.MethodInfo.DeclaringType != null && (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
                                                                            || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any());

            if (hasAuthorize)
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "oauth2"
                                }
                            }
                        ] = new string[] {  }
                    }
                };

            }
        }
    }

    public class JsonIgnoreFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var ignoredProperties = context.MethodInfo.GetParameters()
                .SelectMany(p => p.ParameterType.GetProperties()
                .Where(prop => prop.GetCustomAttribute<JsonIgnoreAttribute>() != null))
                .ToList();

            if (!ignoredProperties.Any()) return;

            foreach (var property in ignoredProperties)
            {
                operation.Parameters = operation.Parameters
                    .Where(p => !p.Name.Equals(property.Name, StringComparison.InvariantCulture))
                    .ToList();
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerExcludeAttribute : Attribute
    {
    }

    public class SwaggerExcludeFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var type = context.Type;
            if (!schema.Properties.Any() || type == null)
            {
                return;
            }

            var excludedPropertyNames = type
                    .GetProperties()
                    .Where(
                        t => t.GetCustomAttribute<SwaggerExcludeAttribute>() != null
                    ).Select(d => d.Name).ToList();

            if (!excludedPropertyNames.Any())
            {
                return;
            }

            var excludedSchemaPropertyKey = schema.Properties
                   .Where(
                        ap => excludedPropertyNames.Any(
                            pn => pn.ToLower() == ap.Key
                        )
                    ).Select(ap => ap.Key);

            foreach (var propertyToExclude in excludedSchemaPropertyKey)
            {
                schema.Properties.Remove(propertyToExclude);
            }
        }
    }   

}

