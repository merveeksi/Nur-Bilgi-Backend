using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NurBilgi.Domain.Identity;
using NurBilgi.Domain.ValueObjects;

namespace NurBilgi.Infrastructure.Persistence.EntityFramework.Seeders;

public class ApplicationDbSeeder
{
    private readonly ILogger<ApplicationDbSeeder> _logger;
    private readonly IServiceProvider _serviceProvider;

    public ApplicationDbSeeder(
        ILogger<ApplicationDbSeeder> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task SeedAsync()
    {
        try
        {
            await SeedDefaultUserAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Veritabanı seed işlemi sırasında bir hata oluştu.");
            throw;
        }
    }

    private async Task SeedDefaultUserAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Varsayılan kullanıcı bilgileri
        var defaultUserEmail = "admin@nurbilgi.com";
        var defaultPassword = "Admin123!";

        // Kullanıcı var mı kontrol et
        var existingUser = await userManager.FindByEmailAsync(defaultUserEmail);
        if (existingUser != null)
        {
            _logger.LogInformation("Varsayılan kullanıcı zaten mevcut.");
            return;
        }

        _logger.LogInformation("Varsayılan kullanıcı oluşturuluyor...");

        // Yeni kullanıcı oluştur
        var fullName = new FullName("Admin", "User");
        var user = ApplicationUser.Create(fullName, defaultUserEmail);
        
        // E-posta doğrulamasını otomatik olarak yap (test için)
        user.EmailConfirmed = true;

        var result = await userManager.CreateAsync(user, defaultPassword);
        if (result.Succeeded)
        {
            _logger.LogInformation("Varsayılan kullanıcı başarıyla oluşturuldu.");
        }
        else
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogError("Varsayılan kullanıcı oluşturulurken hata: {Errors}", errors);
        }
    }
} 