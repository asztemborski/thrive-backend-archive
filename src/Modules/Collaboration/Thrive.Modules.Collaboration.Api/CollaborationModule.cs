using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Thrive.Modules.Collaboration.Infrastructure;
using Thrive.Shared.Abstractions.Modules;

namespace Thrive.Modules.Collaboration.Api;

internal sealed class CollaborationModule : IModule
{
    public string Name => "Collaboration";

    public void Add(IServiceCollection services)
    {
        services.AddInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
       
    }
}