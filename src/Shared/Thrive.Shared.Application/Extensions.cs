using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Thrive.Shared.Application.Behaviours;

namespace Thrive.Shared.Application;

public static class Extensions
{
    public static IServiceCollection AddSharedApplication(this IServiceCollection services,
        IList<Assembly> assemblies)
    {
        services.AddValidatorsFromAssemblies(assemblies);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assemblies.ToArray());
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        return services;
    }
}