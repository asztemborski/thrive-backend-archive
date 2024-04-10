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

    public TokensController(ISender sender) => _sender = sender;
    
    [HttpPost("authenticate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Sign in a user.", Description = "Sign in a user and retrieve auth tokens.")]
    public async Task<Tokens> SignIn(SignInCommand request, CancellationToken cancellationToken)
    {
       return await _sender.Send(request, cancellationToken);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Refresh user token.", Description = "Refresh user's auth tokens.")]
    public async Task<Tokens> RefreshTokens(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        return await _sender.Send(request, cancellationToken);
    }
}