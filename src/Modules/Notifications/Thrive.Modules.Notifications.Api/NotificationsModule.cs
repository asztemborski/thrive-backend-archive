using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Thrive.Modules.Notifications.Core;
using Thrive.Shared.Abstractions.Modules;

namespace Thrive.Modules.Notifications.Api;

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