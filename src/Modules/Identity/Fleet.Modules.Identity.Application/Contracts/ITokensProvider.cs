using Fleet.Modules.Identity.Application.DTOs;
using Fleet.Modules.Identity.Domain.Entities;

namespace Fleet.Modules.Identity.Application.Contracts;

public interface ITokensProvider
{
    Task<Tokens> GenerateAsync(IdentityUser identityUser);
    Task<Tokens> RefreshAsync(string refreshToken);
}