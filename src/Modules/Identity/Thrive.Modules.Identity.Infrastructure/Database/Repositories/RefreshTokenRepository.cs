using Microsoft.EntityFrameworkCore;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Domain.Entities;

namespace Thrive.Modules.Identity.Infrastructure.Database.Repositories;

internal sealed class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly IdentityContext _context;

    public RefreshTokenRepository(IdentityContext context) => _context = context;
    
    public Task<RefreshToken?> GetByToken(string token)
    {
        return _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
    }

    public async Task Delete(RefreshToken token)
    {
        _context.RefreshTokens.Remove(token);
        await _context.SaveChangesAsync();
    }
}