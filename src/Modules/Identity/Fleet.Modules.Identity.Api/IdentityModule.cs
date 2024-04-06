using Fleet.Modules.Identity.Infrastructure;
using Fleet.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fleet.Modules.Identity.Api;

public sealed class IdentityModule : IModule
{
    public string Name => "Identity";
    public void Add(IServiceCollection services)
    {
        services.AddInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseInfrastructure();
    }
}