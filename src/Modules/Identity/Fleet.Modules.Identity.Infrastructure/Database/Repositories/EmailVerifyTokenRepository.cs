using Fleet.Modules.Identity.Application.Contracts;
using Fleet.Modules.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Modules.Identity.Infrastructure.Database.Repositories;

internal sealed class EmailConfirmTokenRepository : IEmailConfirmTokenRepository
{
    private readonly IdentityContext _context;

    public EmailConfirmTokenRepository(IdentityContext context) => _context = context;

    public async Task AddOrUpdateAsync(EmailConfirmationToken token)
    {
        var existingToken = await _context.EmailConfirmationTokens.FirstOrDefaultAsync(ct => ct.Email == token.Email)
                            ?? _context.EmailConfirmationTokens.Add(token).Entity;

        existingToken.UpdateToken(token);
        await _context.SaveChangesAsync();
    }

    public async Task<EmailConfirmationToken?> GetByTokenAsync(string token)
    {
        return await _context.EmailConfirmationTokens.FirstOrDefaultAsync(t => t.Token == token);
    }
}