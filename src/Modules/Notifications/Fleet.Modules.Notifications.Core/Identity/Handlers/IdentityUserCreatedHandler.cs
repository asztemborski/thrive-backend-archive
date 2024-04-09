using Fleet.Modules.Identity.Shared;
using Fleet.Modules.Identity.Shared.Events;
using Fleet.Modules.Notifications.Core.Contracts;
using Fleet.Modules.Notifications.Core.Exceptions;
using Fleet.Modules.Notifications.Core.Mailing.TemplateBuilders;
using MediatR;

namespace Fleet.Modules.Notifications.Core.Identity.Handlers;

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

        if (confirmationUri is null)
        {
            throw new EmptyEmailConfirmationUri();
        }

        var template = new EmailConfirmationTemplateBuilder(notification.Username, confirmationUri).Build();
        await _emailSender.SendAsync(notification.Email, template, cancellationToken);
    }
}