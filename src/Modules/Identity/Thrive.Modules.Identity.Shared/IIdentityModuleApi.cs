using Thrive.Modules.Identity.Shared.DTO;

namespace Thrive.Modules.Identity.Shared;

public interface IIdentityModuleApi
{
    CurrentUserDto GetCurrentAuthenticatedUser();
    Task<string> GenerateEmailConfirmationUri(string email, CancellationToken cancellationToken = default);
}