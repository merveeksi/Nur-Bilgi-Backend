using NurBilgi.Domain.DomainEvents;
using NurBilgi.Domain.ValueObjects;

namespace NurBilgi.Application.Common.Interfaces;

public interface IJwtService
{
    AccessToken Generate(Guid userId, string email, FullName fullName, IList<string> roles,IList<string> permissions);
    Guid GetUserIdFromJwt(string token);
}