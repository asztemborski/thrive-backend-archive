using MediatR;
using Thrive.Modules.Identity.Application.Commands.EmailConfirmationUriCommand;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Shared;

namespace Thrive.Modules.Identity.Api;

internal sealed class IdentityModuleApi : IIdentityModuleApi
{
    private readonly ISender _sender;

    public IdentityModuleApi(ISender sender) => _sender = sender;

    public async Task<string> GenerateEmailConfirmationUri(string email, CancellationToken cancellationToken)
    {
       return await _sender.Send(new EmailConfirmationUriCommand(email), cancellationToken);
    }
}