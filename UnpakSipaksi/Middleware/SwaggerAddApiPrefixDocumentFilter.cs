﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UnpakSipaksi.Middleware
{
    public class SwaggerAddApiPrefixDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (Environment.GetEnvironmentVariable("Mode") == "prod")
            {
                // Add "/api" to all paths
                foreach (var path in swaggerDoc.Paths.ToList())
                {
                    var newPath = "/api" + path.Key;
                    swaggerDoc.Paths.Remove(path.Key);
                    swaggerDoc.Paths.Add(newPath, path.Value);
                }
            }
        }
    }
}
