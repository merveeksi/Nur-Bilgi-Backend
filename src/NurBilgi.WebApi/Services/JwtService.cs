using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Jwt;
using NurBilgi.Domain.Identity;
using NurBilgi.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace NurBilgi.WebApi.Services;

public class JwtService : IJwtService
{
    private readonly JwtManager _jwtManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public JwtService(JwtManager jwtManager, UserManager<ApplicationUser> userManager)
    {
        _jwtManager = jwtManager;
        _userManager = userManager;
    }

    public JwtGenerateTokenResponse GenerateToken(JwtGenerateTokenRequest request)
    {
        // Get user from database to access FullName
        var user = _userManager.FindByIdAsync(request.Id.ToString()).GetAwaiter().GetResult();
        
        if (user == null)
        {
            // Fallback to default valid names if user not found
            user = new ApplicationUser
            {
                Id = request.Id,
                Email = request.Email,
                FullName = new FullName("User", "Default")
            };
        }

        // Generate token
        var token = _jwtManager.GenerateToken(user, request.Roles);
        
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
