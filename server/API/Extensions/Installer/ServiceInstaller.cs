using Middleware.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Services.Interfaces;
using Middleware.Services;
using Core.Interfaces;
using AutoMapper;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using API.Errors;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddDbContext<IdentityDataContext>(
                optionsBuilder => optionsBuilder.UseNpgsql(configuration.GetConnectionString("IdentityConnection"))
            );
            services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
            {
                var configurationOptions = ConfigurationOptions.Parse(configuration.GetConnectionString("RedisConnection"));
                return ConnectionMultiplexer.Connect(configurationOptions);
            });

            var builder = services.AddIdentityCore<AppUser>();
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<IdentityDataContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(bearerOptions =>
                {
                    bearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                        ValidIssuer = configuration["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });

            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductBrandService, ProductBrandService>();
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IJWTTokenService, JWTTokenService>();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(error => error.Value.Errors.Count > 0)
                    .SelectMany(kvp => kvp.Value.Errors)
                    .Select(modelError => modelError.ErrorMessage).ToArray();

                    var errorResponse = new APIValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "SkynER API", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                options.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new []{ "Bearer" } }
                };

                options.AddSecurityRequirement(securityRequirement);
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
        }
    }
}