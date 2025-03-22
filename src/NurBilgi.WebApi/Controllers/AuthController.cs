using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NurBilgi.Application.Features.Auth.Commands.Login;
using NurBilgi.Application.Features.Auth.Commands.Register;
using NurBilgi.Application.Features.Auth.Commands.ResendEmailVerificationEmail;
using NurBilgi.Application.Features.Auth.Commands.VerifyEmail;

namespace NurBilgi.WebApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ApiControllerBase
{
    public AuthController(ISender mediator) : base(mediator)
    {
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthLoginCommand command, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(command, cancellationToken));


    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthRegisterCommand command, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(command, cancellationToken));

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail(AuthVerifyEmailCommand command, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(command, cancellationToken));

    [HttpPost("resend-email-verification")]
    public async Task<IActionResult> ResendEmailVerification(AuthReSendEmailVerificationEmailCommand command, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(command, cancellationToken));
}

  