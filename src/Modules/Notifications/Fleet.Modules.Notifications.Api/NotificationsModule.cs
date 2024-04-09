using Fleet.Modules.Notifications.Core;
using Fleet.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fleet.Modules.Notifications.Api;

internal sealed class NotificationsModule : IModule
{
    public string Name => "Notifications";

    public void Add(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}