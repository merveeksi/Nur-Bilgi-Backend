using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Jwt;
using NurBilgi.Domain.Identity;
using NurBilgi.Domain.ValueObjects;

namespace NurBilgi.WebApi.Services;

public class JwtService : IJwtService
{
    private readonly JwtManager _jwtManager;

    public JwtService(JwtManager jwtManager)
    {
        _jwtManager = jwtManager;
    }

    public JwtGenerateTokenResponse GenerateToken(JwtGenerateTokenRequest request)
    {
        // Create ApplicationUser for JwtManager
        var applicationUser = new ApplicationUser
        {
            Id = request.Id,
            Email = request.Email,
            FullName = new FullName("", "") // Using empty strings as we don't have names in the request
        };

        // Generate token
        var token = _jwtManager.GenerateToken(applicationUser, request.Roles);
        
        // Return token response with expiration time
        return new JwtGenerateTokenResponse(
            token, 
            DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30)) // Using a standard 30 min expiration
        );
    }

    public bool ValidateToken(string token)
    {
        return _jwtManager.ValidateToken(token);
    }

    public long GetUserIdFromJwt(string token)
    {
        return _jwtManager.GetUserIdFromJwt(token);
    }
}
