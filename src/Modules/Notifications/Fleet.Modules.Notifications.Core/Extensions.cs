using Fleet.Modules.Notifications.Core.Contracts;
using Fleet.Modules.Notifications.Core.Options.Setups;
using Fleet.Modules.Notifications.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fleet.Modules.Notifications.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.ConfigureOptions<MailingOptionsSetup>();
        services.AddScoped<IEmailSender, EmailSender>();
        return services;
    }
}