using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Thrive.Modules.Identity.Application;
using Thrive.Modules.Identity.Infrastructure;
using Thrive.Modules.Identity.Shared;
using Thrive.Shared.Abstractions.Modules;

namespace Thrive.Modules.Identity.Api;

internal sealed class IdentityModule : IModule
{
    public string Name => "Identity";

    public void Add(IServiceCollection services)
    {
        services.AddApplication();
        services.AddInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseInfrastructure();
    }
}