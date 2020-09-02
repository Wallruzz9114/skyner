using System;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Middleware.Data;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var dataContext = services.GetRequiredService<DataContext>();
                    await dataContext.Database.MigrateAsync();
                    await SeedDataContext.SeedDataAsync(dataContext, loggerFactory);

                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var identityDataContext = services.GetRequiredService<IdentityDataContext>();
                    await identityDataContext.Database.MigrateAsync();
                    await SeedIdentityDataContext.SeedIdentityDataAsync(userManager);
                }
                catch (Exception exception)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(exception, "An error occured during migration");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
