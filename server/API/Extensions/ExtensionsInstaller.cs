using System;
using System.Collections.Generic;
using System.Linq;
using API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ExtensionsInstaller
    {
        public static void InstallServicesInAssembly(IServiceCollection services, IConfiguration configuration)
        {
            List<IServiceInstaller> installers = typeof(Startup).Assembly.ExportedTypes
                .Where(type => typeof(IServiceInstaller).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IServiceInstaller>()
                .ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}