using Fleet.Modules.Notifications.Core.Mailing;

namespace Fleet.Modules.Notifications.Core.Contracts;

internal interface IEmailSender
{
    Task SendAsync(string receiver, EmailTemplate emailTemplate, CancellationToken cancellationToken);
}