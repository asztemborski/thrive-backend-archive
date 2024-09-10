using MediatR;
using Thrive.Modules.Identity.Application.Contracts;

namespace Thrive.Modules.Identity.Application.Commands.Logout;

internal sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LogoutCommandHandler(IRefreshTokenRepository refreshTokenRepository) =>
        _refreshTokenRepository = refreshTokenRepository;
    
    
    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.GetByToken(request.RefreshToken);

        if (refreshToken is null) return;

        await _refreshTokenRepository.Delete(refreshToken);
    }
}