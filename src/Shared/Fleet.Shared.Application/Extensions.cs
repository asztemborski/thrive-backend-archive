using System.Reflection;
using Fleet.Shared.Application.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Fleet.Shared.Application;

public static class Extensions
{
    public static IServiceCollection AddSharedApplication(this IServiceCollection services,
        IList<Assembly> assemblies)
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
        services.AddValidatorsFromAssemblies(assemblies);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assemblies.ToArray());
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        return services;
    }
}