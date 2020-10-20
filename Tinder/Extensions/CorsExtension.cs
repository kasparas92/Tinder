using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Tinder.API.Extensions
{
    public static class CorsExtension
    {
        public static void ConfigureCors(this IServiceCollection service)
        {
            service.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }
        public static void UseCorsWithPolicy(this IApplicationBuilder builder )
        {
            builder.UseCors("CorsPolicy");
        }
    }
}
