using MediatR;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Email;
using NurBilgi.Application.Common.Models.Identity;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Auth.Commands.ResendEmailVerificationEmail;

public class AuthReSendEmailVerificationEmailCommandHandler : IRequestHandler<AuthReSendEmailVerificationEmailCommand, ResponseDto<string>>
{
    private readonly IIdentityService _identityService;
    private readonly IEmailService _emailService;
    public AuthReSendEmailVerificationEmailCommandHandler(IIdentityService identityService, IEmailService emailService)
    {
        _identityService = identityService;
        _emailService = emailService;
    }

    public async Task<ResponseDto<string>> Handle(AuthReSendEmailVerificationEmailCommand request, CancellationToken cancellationToken)
    {
        var response = await _identityService.CreateEmailTokenAsync(new IdentityCreateEmailTokenRequest(request.Email), cancellationToken);

        var emailVerificationDto = new EmailVerificationDto(request.Email, response.Token);

        await _emailService.EmailVerificationAsync(emailVerificationDto, cancellationToken);

        return new ResponseDto<string>(data: response.Token, message: "Email verification email sent.");
    }


}