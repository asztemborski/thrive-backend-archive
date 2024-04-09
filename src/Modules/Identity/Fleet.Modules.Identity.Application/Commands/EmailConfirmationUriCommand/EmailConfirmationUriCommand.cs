using MediatR;

namespace Fleet.Modules.Identity.Application.Commands.EmailConfirmationUriCommand;

public sealed record EmailConfirmationUriCommand(string Email) : IRequest;