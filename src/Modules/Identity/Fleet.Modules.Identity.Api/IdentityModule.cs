using Fleet.Modules.Identity.Infrastructure;
using Fleet.Modules.Identity.Shared;
using Fleet.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fleet.Modules.Identity.Api;

internal sealed class IdentityModule : IModule
{
    public string Name => "Identity";

    public void Add(IServiceCollection services)
    {
        services.AddTransient<IIdentityModuleApi, IdentityModuleApi>();
        services.AddInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseInfrastructure();
    }
}