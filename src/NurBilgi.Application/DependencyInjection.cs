using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NurBilgi.Application.Common.PiplineBehaviors;

namespace NurBilgi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {

            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

}