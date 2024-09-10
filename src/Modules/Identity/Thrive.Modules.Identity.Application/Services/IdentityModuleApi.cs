using MediatR;
using Thrive.Modules.Identity.Application.Commands.EmailConfirmationUriCommand;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Shared;
using Thrive.Modules.Identity.Shared.DTO;

namespace Thrive.Modules.Identity.Application.Services;

internal sealed class IdentityModuleApi : IIdentityModuleApi
{
    private readonly ISender _sender;
    private readonly ICurrentUserService _currentUserService;

    public IdentityModuleApi(ISender sender, ICurrentUserService currentUserService)
    {
        _sender = sender;
        _currentUserService = currentUserService;
    }

    public CurrentUserDto GetCurrentAuthenticatedUser() => _currentUserService.GetCurrentUser();

    public async Task<string> GenerateEmailConfirmationUri(string email, CancellationToken cancellationToken)
        => await _sender.Send(new EmailConfirmationUriCommand(email), cancellationToken);
    
}