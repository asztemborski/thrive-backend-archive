using Microsoft.Extensions.DependencyInjection;
using Thrive.Modules.Notifications.Core.Contracts;
using Thrive.Modules.Notifications.Core.Options.Setups;
using Thrive.Modules.Notifications.Core.Services;

namespace Thrive.Modules.Notifications.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.ConfigureOptions<MailingOptionsSetup>();
        services.AddScoped<IEmailSender, EmailSender>();
        return services;
    }
}