using MediatR;
using Thrive.Modules.Identity.Application.Commands.EmailConfirmationUriCommand;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Shared;

namespace Thrive.Modules.Identity.Api;

internal sealed class IdentityModuleApi : IIdentityModuleApi
{
    private readonly ISender _sender;
    private readonly IConfirmationUriRequestStorage _confirmationUriRequestStorage;

    public IdentityModuleApi(ISender sender, IConfirmationUriRequestStorage confirmationUriRequestStorage)
    {
        _sender = sender;
        _confirmationUriRequestStorage = confirmationUriRequestStorage;
    }

    public async Task<string?> GenerateEmailConfirmationUri(string email, CancellationToken cancellationToken)
    {
        await _sender.Send(new EmailConfirmationUriCommand(email), cancellationToken);
        return _confirmationUriRequestStorage.RetrieveUri(email);
    }
}