using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Identity;
using NurBilgi.Application.Common.Models.Jwt;
using NurBilgi.Application.Features.Auth.Commands.Register;
using NurBilgi.Domain.Identity;
using NurBilgi.Domain.Settings;
using NurBilgi.Domain.ValueObjects;

namespace NurBilgi.Infrastructure.Services;

public sealed class IdentityManager: IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;
    private readonly JwtSettings _jwtSettings;
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public IdentityManager(UserManager<ApplicationUser> userManager, IJwtService jwtService,
        JwtSettings jwtSettings, IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _jwtSettings = jwtSettings;
        _context = context;
        _currentUserService = currentUserService;
    }
    
    // Kullanıcının kimliğini doğrular.
    public async Task<bool> AuthenticateAsync(IdentityAuthenticateRequest request, CancellationToken cancellationToken)
    {
        // Kullanıcıyı e-posta adresine göre bul.
        var user = await _userManager.FindByEmailAsync(request.Email);

        // Kullanıcı bulunamazsa false döndür.
        if (user is null) return false;

        // Kullanıcının parolasını kontrol et ve sonucu döndür.
        return await _userManager.CheckPasswordAsync(user, request.Password);
    }

    // E-posta adresinin veritabanında olup olmadığını kontrol eder.
    public Task<bool> CheckEmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        return _userManager
        .Users
        .AnyAsync(x => x.Email == email, cancellationToken);
    }

    public Task<bool> CheckIfEmailVerifiedAsync(string email, CancellationToken cancellationToken)
    {
        return _userManager
        .Users
        .AnyAsync(x => x.Email == email && x.EmailConfirmed, cancellationToken);
    }

    public async Task<bool> CheckSecurityStampAsync(Guid userId, string securityStamp, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        return string.Equals(securityStamp, user.SecurityStamp);
    }

    public async Task<IdentityCreateEmailTokenResponse> CreateEmailTokenAsync(IdentityCreateEmailTokenRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        return new IdentityCreateEmailTokenResponse(token);
    }

    // Kullanıcının giriş yapmasını sağlar.
    public async Task<IdentityLoginResponse> LoginAsync(IdentityLoginRequest request, CancellationToken cancellationToken)
    {
        // Kullanıcıyı e-posta adresine göre bul.
        var user = await _userManager.FindByEmailAsync(request.Email);

        // Kullanıcının rollerini al.
        var roles = await _userManager.GetRolesAsync(user);

        // JWT oluşturma isteği oluştur.
        var jwtRequest = new JwtGenerateTokenRequest(user.Id, user.Email, roles);

        // "Beni hatırla" seçeneği dikkate alınacak şekilde değişiklikler backend'de yapılacaktır
        // Burada tokenin ömrünü belirlemek için JwtSettings kullanılabilir
        
        // JWT oluştur.
        var jwtResponse = _jwtService.GenerateToken(jwtRequest);
        
        // Giriş yanıtını döndür.
        return new IdentityLoginResponse(jwtResponse.Token, jwtResponse.ExpiresAt);
    }
    

    // Yeni bir kullanıcı kaydeder.
    public async Task<IdentityRegisterResponse> RegisterAsync(IdentityRegisterRequest request, CancellationToken cancellationToken)
    {
        // Ensure first name and last name are valid according to the FullName class requirements
        var firstName = string.IsNullOrWhiteSpace(request.FirstName) || !FullName.IsValid(request.FirstName) 
            ? "User" : request.FirstName;
        var lastName = string.IsNullOrWhiteSpace(request.LastName) || !FullName.IsValid(request.LastName) 
            ? "Default" : request.LastName;
        
        var fullName = new FullName(firstName, lastName);
        var user = ApplicationUser.Create(fullName, request.Email);

        var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                CreateAndThrowValidationException(result.Errors);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        return new IdentityRegisterResponse(user.Id, user.Email, token);
    }

    public async Task<IdentityVerifyEmailResponse> VerifyEmailAsync(IdentityVerifyEmailRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        // var decodedToken = HttpUtility.UrlDecode(request.Token);

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);

        if (!result.Succeeded)
            CreateAndThrowValidationException(result.Errors);

        return new IdentityVerifyEmailResponse(user.Email);
    }

    // Doğrulama hatası oluşturur ve fırlatır.
    private void CreateAndThrowValidationException(IEnumerable<IdentityError> errors)
    {
        // Hata mesajlarını ve özelliklerini içeren yeni bir doğrulama hatası oluştur.
        var errorMessages = errors
        .Select(x => new ValidationFailure(x.Code, x.Description))
        .ToArray();

        // Doğrulama hatasını fırlat.
        throw new ValidationException(errorMessages);
    }

    public async Task<string> RegisterAsync(AuthRegisterCommand command, CancellationToken cancellationToken)
    {
        var user = ApplicationUser.Create(command.FullName, command.Email);

        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }

        return user.Id.ToString();
    }
}