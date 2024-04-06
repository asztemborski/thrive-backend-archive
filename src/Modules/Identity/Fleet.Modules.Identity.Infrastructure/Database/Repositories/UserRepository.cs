using Fleet.Modules.Identity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using IdentityUser = Fleet.Modules.Identity.Domain.Entities.IdentityUser;

namespace Fleet.Modules.Identity.Infrastructure.Database.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IdentityContext _context;

    public UserRepository(IdentityContext context)
    {
        _context = context;
    }

    public async Task<IdentityUser?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email.Address.Value == email);
    }

    public async Task<IdentityUser?> GetWithRefreshTokensAsync(Guid id)
    {
        return await _context.Users.Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task CreateAsync(IdentityUser identityUser)
    {
        await _context.Users.AddAsync(identityUser);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(IdentityUser identityUser)
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await _context.Users.AnyAsync(u => u.Email.Address.Value == email);
    }

    public async Task<bool> IsUsernameUniqueAsync(string username)
    {
        return !await _context.Users.AnyAsync(u => u.Username.Value == username);
    }
}