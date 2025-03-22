using NurBilgi.Domain.ValueObjects;

namespace NurBilgi.Application.Common.Models.Identity;

public class IdentityRegisterRequest
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public IdentityRegisterRequest(string email, string password, FullName? fullName)
    {
        Email = email;
        Password = password;
        FirstName = fullName?.FirstName;
        LastName = fullName?.LastName;
    }
}