using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Thrive.Modules.Identity.Application.Commands.RefreshTokensCommand;
using Thrive.Modules.Identity.Application.Commands.SignInCommand;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Application.DTOs;

namespace Thrive.Modules.Identity.Api.Controllers;

public sealed class TokensController : BaseController
{
    private readonly ISender _sender;
    private readonly ITokensRequestStorage _tokensRequestStorage;

    public TokensController(ISender sender, ITokensRequestStorage tokensRequestStorage)
    {
        _sender = sender;
        _tokensRequestStorage = tokensRequestStorage;
    }

    [HttpPost("authenticate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Sign in a user.", Description = "Sign in a user and retrieve auth tokens.")]
    public async Task<Tokens?> SignIn(SignInCommand request, CancellationToken cancellationToken)
    {
        await _sender.Send(request, cancellationToken);
        return _tokensRequestStorage.RetrieveTokens(request.Email);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Refresh user token.", Description = "Refresh user's auth tokens.")]
    public async Task<Tokens?> RefreshTokens(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        await _sender.Send(request, cancellationToken);
        return _tokensRequestStorage.RetrieveTokens(request.RefreshToken);
    }
}