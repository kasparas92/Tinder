using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tinder.DataModel.Context;
using Tinder.DataModel.Data;

namespace Tinder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var scope = host.Services.CreateScope();
            var service = scope.ServiceProvider;
            try
            {
                var context = service.GetRequiredService<TinderContext>();
                context.Database.Migrate();
                Seed.SeedUsers(context);
            }
            catch(Exception exception)
            {
                var logger = service.GetRequiredService<ILogger<Program>>();
                logger.LogError(exception, "Migration Failed");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
