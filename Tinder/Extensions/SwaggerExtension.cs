using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace Tinder.API.Extensions
{
    public static class SwaggerExtension
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Tinder" });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                options.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };
                options.AddSecurityRequirement(securityRequirement);
            });
        }

        public static void UseClientApi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            { 
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tinder v1");
            });
        }
    }
}
