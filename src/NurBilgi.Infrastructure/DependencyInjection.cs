using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Domain.Identity;
using NurBilgi.Infrastructure.Persistence.EntityFramework.Contexts;
using NurBilgi.Infrastructure.Services;

namespace NurBilgi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsHistoryTable("__ef_migrations_history"))
                    .UseSnakeCaseNamingConvention());
                
        services
            .AddScoped<IApplicationDbContext,ApplicationDbContext>();
        
        
        services.AddScoped<ICacheInvalidator, CacheInvalidator>();
        

        // Add localization
        services.AddLocalization(options => options.ResourcesPath = "Resources");

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
          {
              options.User.RequireUniqueEmail = true;

              options.Password.RequireNonAlphanumeric = false;
              options.Password.RequireUppercase = false;
              options.Password.RequireLowercase = false;
              options.Password.RequireDigit = false;
              options.Password.RequiredUniqueChars = 0;
              options.Password.RequiredLength = 8;
          })
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();
        
        services.AddScoped<IIdentityService, IdentityManager>();

        return services;

    }
}