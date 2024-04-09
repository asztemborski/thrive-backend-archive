using Thrive.Modules.Identity.Domain.Entities;

namespace Thrive.Modules.Identity.Domain.Repositories;

public interface IUserRepository
{
    Task<IdentityUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IdentityUser?> GetWithRefreshTokensAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IdentityUser> CreateAsync(IdentityUser identityUser, CancellationToken cancellationToken = default);
    Task UpdateAsync(CancellationToken cancellationToken = default);
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> IsUsernameUniqueAsync(string username, CancellationToken cancellationToken = default);
}