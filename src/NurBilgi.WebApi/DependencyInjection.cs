using System.Reflection;
using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.PiplineBehaviors;
using NurBilgi.Domain.Settings;
using NurBilgi.Infrastructure.Services;
using NurBilgi.WebApi.Services;

namespace NurBilgi.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
                               {
                                   options.AddPolicy("AllowAll",
                                       builder => builder
                                           .AllowAnyMethod()
                                           .AllowCredentials()
                                           .SetIsOriginAllowed((host) => true)
                                           .AllowAnyHeader());
                               });

       // services.AddSwaggerWithVersion();
        
        services.AddEndpointsApiExplorer();

        services.AddMemoryCache();

        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserService, CurrentUserManager>();

        services.Configure<JwtSettings>(
            configuration.GetSection(nameof(JwtSettings)));
        
        // AddIdentity kaldırıldı - Infrastructure katmanında zaten yapılandırılmış

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        });

       

        services.AddScoped<CacheInvalidator>();

        services.AddScoped<JwtManager>();
        services.AddScoped<IJwtService, JwtService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var secretKey = configuration["JwtSettings:SecretKey"];

            if (string.IsNullOrEmpty(secretKey))
                throw new ArgumentNullException("JwtSettings:SecretKey is not set.");

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
}