using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Infrastructure.Persistence.EntityFramework.Contexts;

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
        
        return services;
    }
}