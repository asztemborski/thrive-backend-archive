using Microsoft.EntityFrameworkCore;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Domain.Entities;

namespace Thrive.Modules.Identity.Infrastructure.Database.Repositories;

internal sealed class EmailConfirmTokenRepository : IEmailConfirmTokenRepository
{
    private readonly IdentityContext _context;

    public EmailConfirmTokenRepository(IdentityContext context) => _context = context;

    public async Task AddOrUpdateAsync(EmailConfirmationToken token, CancellationToken cancellationToken)
    {
        var existingToken = await _context.EmailConfirmationTokens.FirstOrDefaultAsync(ct => ct.Email == token.Email,
                                cancellationToken) ?? _context.EmailConfirmationTokens.Add(token).Entity;

        existingToken.UpdateToken(token);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<EmailConfirmationToken?> GetByTokenAsync(string token, CancellationToken cancellationToken)
    {
        return await _context.EmailConfirmationTokens.FirstOrDefaultAsync(t => t.Token == token,
            cancellationToken);
    }
}