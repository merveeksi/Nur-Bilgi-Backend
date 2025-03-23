using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NurBilgi.Application.Features.Auth.Commands.Login;
using NurBilgi.Application.Features.Auth.Commands.Register;
using NurBilgi.Application.Features.Auth.Commands.ResendEmailVerificationEmail;
using NurBilgi.Application.Features.Auth.Commands.VerifyEmail;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace NurBilgi.WebApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ApiControllerBase
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ISender mediator, ILogger<AuthController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthLoginCommand command, CancellationToken cancellationToken)
    {
        // Log the incoming request
        _logger.LogInformation("Login request: {RequestBody}", JsonSerializer.Serialize(command));
        
        // Check if the request is valid
        if (string.IsNullOrEmpty(command.Email) || string.IsNullOrEmpty(command.Password))
        {
            _logger.LogWarning("Invalid login request: Email or password is missing");
            return BadRequest(new { Message = "E-posta ve şifre zorunludur." });
        }

        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

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

  