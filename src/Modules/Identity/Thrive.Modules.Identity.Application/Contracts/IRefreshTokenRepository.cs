using Thrive.Modules.Identity.Domain.Entities;

namespace Thrive.Modules.Identity.Application.Contracts;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByToken(string token);
    Task Delete(RefreshToken token);
}