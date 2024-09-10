using Microsoft.Extensions.DependencyInjection;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Application.Services;
using Thrive.Modules.Identity.Shared;

namespace Thrive.Modules.Identity.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddTransient<IIdentityModuleApi, IdentityModuleApi>();
        
        return services;
    }
}