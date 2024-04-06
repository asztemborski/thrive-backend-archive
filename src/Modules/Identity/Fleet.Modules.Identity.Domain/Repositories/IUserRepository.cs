using Fleet.Modules.Identity.Domain.Entities;

namespace Fleet.Modules.Identity.Domain.Repositories;

public interface IUserRepository
{
    Task<IdentityUser?> GetByEmailAsync(string email);
    Task<IdentityUser?> GetWithRefreshTokensAsync(Guid id);
    Task CreateAsync(IdentityUser identityUser);
    Task UpdateAsync(IdentityUser identityUser);
    Task<bool> IsEmailUniqueAsync(string email);
    Task<bool> IsUsernameUniqueAsync(string username);
}