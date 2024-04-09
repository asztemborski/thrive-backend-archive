using Thrive.Modules.Identity.Domain.Entities;

namespace Thrive.Modules.Identity.Application.Contracts;

public interface IEmailConfirmTokenRepository
{
    Task AddOrUpdateAsync(EmailConfirmationToken token);
    Task<EmailConfirmationToken?> GetByTokenAsync(string token);
}