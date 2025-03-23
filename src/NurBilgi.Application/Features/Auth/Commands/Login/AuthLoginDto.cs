using System.ComponentModel.DataAnnotations;
using NurBilgi.Application.Common.Models.Identity;

namespace NurBilgi.Application.Features.Auth.Commands.Login;

public class UserDto
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    
    public UserDto(long id, string email, string firstName, string lastName)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
}

public class AuthLoginDto
{
    public string Token { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public UserDto User { get; set; }

    public AuthLoginDto()
    {
        
    }

    public AuthLoginDto(string token, DateTimeOffset expiresAt, UserDto user)
    {
        Token = token;
        ExpiresAt = expiresAt;
        User = user;
    }

    public static AuthLoginDto FromIdentityLoginResponse(IdentityLoginResponse response)
    {
        // Burada kullanıcı bilgisi eksik, bu yüzden null geçiyoruz
        // Bu metodu çağıran yerde kullanıcı bilgilerini eklemek gerekecek
        return new AuthLoginDto(response.Token, response.ExpiresAt, null);
    }
}
