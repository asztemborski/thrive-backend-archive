using Thrive.Modules.Identity.Domain.Entities;

namespace Thrive.Modules.Identity.Application.Contracts;

public interface IEmailConfirmTokenRepository
{
    Task AddOrUpdateAsync(EmailConfirmationToken token, CancellationToken cancellationToken = default);
    Task<EmailConfirmationToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
}