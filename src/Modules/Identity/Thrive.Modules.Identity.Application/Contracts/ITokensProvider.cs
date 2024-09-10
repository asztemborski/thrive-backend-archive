using System.Security.Claims;
using Thrive.Modules.Identity.Application.DTOs;
using Thrive.Modules.Identity.Domain.Entities;

namespace Thrive.Modules.Identity.Application.Contracts;

public interface ITokensProvider
{
    Task<Tokens> GenerateAccessAsync(IdentityUser identityUser, CancellationToken cancellationToken = default);
    Task<Tokens> RefreshAsync(string refreshToken, CancellationToken cancellationToken = default);
    EmailConfirmationToken GenerateEmailConfirmationTokenAsync(string email);
}