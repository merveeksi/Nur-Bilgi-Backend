using NurBilgi.Application.Common.Models.Jwt;
using NurBilgi.Domain.ValueObjects;

namespace NurBilgi.Application.Common.Interfaces;

public interface IJwtService
{
    JwtGenerateTokenResponse GenerateToken(JwtGenerateTokenRequest request);
    bool ValidateToken(string token);
    long GetUserIdFromJwt(string token);
}