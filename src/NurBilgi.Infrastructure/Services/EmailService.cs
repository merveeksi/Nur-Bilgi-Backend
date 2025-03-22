using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Email;
using Microsoft.Extensions.Logging;

namespace NurBilgi.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public async Task EmailVerificationAsync(EmailVerificationDto emailVerificationDto, CancellationToken cancellationToken)
    {
        // Simüle edilmiş e-posta gönderimi - gerçek uygulamada SMTP veya bir e-posta servisi kullanılmalıdır
        _logger.LogInformation("E-posta doğrulama bağlantısı gönderiliyor: {Email}, Token: {Token}", 
            emailVerificationDto.Email, 
            emailVerificationDto.Token);

        // Gerçek e-posta gönderimi burada yapılmalıdır
        // SendGrid, MailKit, SMTP veya benzeri servisler entegre edilebilir
        
        // Simüle edilmiş görev beklemesi
        await Task.Delay(100, cancellationToken);
    }
} 