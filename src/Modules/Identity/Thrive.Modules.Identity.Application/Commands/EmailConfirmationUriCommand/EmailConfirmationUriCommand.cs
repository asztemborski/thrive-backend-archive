using MediatR;

namespace Thrive.Modules.Identity.Application.Commands.EmailConfirmationUriCommand;

public sealed record EmailConfirmationUriCommand(string Email) : IRequest;