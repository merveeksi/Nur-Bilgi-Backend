using NurBilgi.Application.Common.Models.Email;

namespace NurBilgi.Application.Common.Interfaces;

public interface IEmailService
{
    Task EmailVerificationAsync(EmailVerificationDto emailVerificationDto, CancellationToken cancellationToken);
 
}