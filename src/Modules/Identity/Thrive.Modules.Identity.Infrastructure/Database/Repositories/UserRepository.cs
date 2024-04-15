using Microsoft.EntityFrameworkCore;
using Thrive.Modules.Identity.Domain.Repositories;
using IdentityUser = Thrive.Modules.Identity.Domain.Entities.IdentityUser;

namespace Thrive.Modules.Identity.Infrastructure.Database.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly IdentityContext _context;

    public UserRepository(IdentityContext context) => _context = context;

    public async Task<IdentityUser?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email.Address.Value == email, 
            cancellationToken);
    }

    public async Task<IdentityUser?> GetWithRefreshTokensAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users.Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<IdentityUser> CreateAsync(IdentityUser identityUser, CancellationToken cancellationToken)
    {
        var user = await _context.Users.AddAsync(identityUser, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user.Entity;
    }

    public async Task UpdateAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<(bool isEmailUnique, bool isUsernameUnique)> IsUnique(string email, string username, 
        CancellationToken cancellationToken)
    {
        var user = await _context.Users.Select(u => new
            {
                Email = u.Email.Address.Value, 
                Username =  u.Username.Value
            })
            .FirstOrDefaultAsync(u => u.Email == email || u.Username == username, cancellationToken);
        
        return user is null ? (true, true) : (user.Email != email, user.Username != username);
    }
}