using MediatR;

namespace Thrive.Modules.Identity.Application.Commands.ConfirmEmailCommand;

public sealed record ConfirmEmailCommand(string ConfirmationToken) : IRequest;