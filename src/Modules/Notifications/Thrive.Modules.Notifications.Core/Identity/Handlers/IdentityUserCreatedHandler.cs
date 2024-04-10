using MediatR;
using Thrive.Modules.Identity.Shared;
using Thrive.Modules.Identity.Shared.Events;
using Thrive.Modules.Notifications.Core.Contracts;
using Thrive.Modules.Notifications.Core.Mailing.TemplateBuilders;

namespace Thrive.Modules.Notifications.Core.Identity.Handlers;

internal sealed class IdentityUserCreatedHandler : INotificationHandler<IdentityUserCreatedEvent>
{
    private readonly IIdentityModuleApi _identityModuleApi;
    private readonly IEmailSender _emailSender;

    public IdentityUserCreatedHandler(IEmailSender emailSender, IIdentityModuleApi identityModuleApi)
    {
        _emailSender = emailSender;
        _identityModuleApi = identityModuleApi;
    }

    public async Task Handle(IdentityUserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var confirmationUri =
            await _identityModuleApi.GenerateEmailConfirmationUri(notification.Email, cancellationToken);
        
        var template = new EmailConfirmationTemplateBuilder(notification.Username, confirmationUri).Build();
        await _emailSender.SendAsync(notification.Email, template, cancellationToken);
    }
}