using Thrive.Modules.Identity.Domain.Entities;

namespace Thrive.Modules.Identity.Domain.Repositories;

public interface IUserRepository
{
    Task<IdentityUser?> GetByEmailAsync(string email);
    Task<IdentityUser?> GetWithRefreshTokensAsync(Guid id);
    Task<IdentityUser> CreateAsync(IdentityUser identityUser);
    Task UpdateAsync(IdentityUser identityUser);
    Task<bool> IsEmailUniqueAsync(string email);
    Task<bool> IsUsernameUniqueAsync(string username);
}