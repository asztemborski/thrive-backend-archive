using Thrive.Modules.Identity.Application.DTOs;
using Thrive.Modules.Identity.Domain.Entities;

namespace Thrive.Modules.Identity.Application.Contracts;

public interface ITokensProvider
{
    Task<Tokens> GenerateAccessAsync(IdentityUser identityUser);
    Task<Tokens> RefreshAsync(string refreshToken);
    EmailConfirmationToken GenerateEmailConfirmationTokenAsync(string email);
}