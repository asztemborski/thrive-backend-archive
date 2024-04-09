using Thrive.Modules.Notifications.Core.Mailing;

namespace Thrive.Modules.Notifications.Core.Contracts;

internal interface IEmailSender
{
    Task SendAsync(string receiver, EmailTemplate emailTemplate, CancellationToken cancellationToken = default);
}