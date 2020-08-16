using Middleware.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Services.Interfaces;
using Middleware.Services;
using Core.Interfaces;
using AutoMapper;
using API.Helpers;

namespace API.Extensions.Installer
{
    public class ServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>(
                optionsBuilder => optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductBrandService, ProductBrandService>();
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddAutoMapper(typeof(MappingProfiles));
        }
    }
}