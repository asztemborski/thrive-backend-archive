using Fleet.Modules.Identity.Application.Commands.SignUpCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Fleet.Modules.Identity.Api.Controllers;

public sealed class IdentityController : BaseController
{
    private readonly ISender _sender;

    public IdentityController(ISender sender) => _sender = sender;

    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Registers a new user.", Description = "Register a new user based on the request.")]
    public async Task SignUp(SignUpCommand request, CancellationToken cancellationToken)
    {
        await _sender.Send(request, cancellationToken);
    }
}