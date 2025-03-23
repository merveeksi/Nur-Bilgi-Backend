using Microsoft.EntityFrameworkCore;
using NurBilgi.Infrastructure.Persistence.EntityFramework.Contexts;
using NurBilgi.Infrastructure.Persistence.EntityFramework.Seeders;

namespace NurBilgi.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (dbContext.Database.GetPendingMigrations().Any())
            dbContext.Database.Migrate();

        // Seed veritabanı
        var seeder = scope.ServiceProvider.GetRequiredService<ApplicationDbSeeder>();
        seeder.SeedAsync().GetAwaiter().GetResult();

        return app;
    }
}