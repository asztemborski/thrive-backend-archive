using Fleet.Modules.Identity.Domain.Entities;

namespace Fleet.Modules.Identity.Application.Contracts;

public interface IEmailConfirmTokenRepository
{
    Task AddOrUpdateAsync(EmailConfirmationToken token);
    Task<EmailConfirmationToken?> GetByTokenAsync(string token);
}