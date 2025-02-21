using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.WebApi.Services;

public sealed class CurrentUserManager : ICurrentUserService
{
    public long? UserId => 12345678;
}