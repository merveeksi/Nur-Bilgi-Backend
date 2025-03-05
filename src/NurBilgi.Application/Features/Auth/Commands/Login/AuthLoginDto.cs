using NurBilgi.Domain.DomainEvents;

namespace NurBilgi.Application.Features.Auth.Commands.Login;

public sealed record AuthLoginResponse(AccessToken AccessToken, RefreshToken RefreshToken);