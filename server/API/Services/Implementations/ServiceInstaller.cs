using Middleware.Data;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Services.Implementations
{
    public class ServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>(
                optionsBuilder => optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            );
        }
    }
}