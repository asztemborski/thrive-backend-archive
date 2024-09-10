using MediatR;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Application.DTOs;

namespace Thrive.Modules.Identity.Application.Commands.RefreshTokens;

internal sealed class RefreshTokensCommandHandler : IRequestHandler<RefreshTokensCommand, Tokens>
{
    private readonly ITokensProvider _tokensProvider;

    public RefreshTokensCommandHandler(ITokensProvider tokensProvider)
    {
        _tokensProvider = tokensProvider;
    }

    public async Task<Tokens> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        return await _tokensProvider.RefreshAsync(request.RefreshToken, cancellationToken);
    }
}