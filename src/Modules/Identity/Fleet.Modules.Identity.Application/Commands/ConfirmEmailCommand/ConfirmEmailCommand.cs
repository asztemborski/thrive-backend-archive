using MediatR;

namespace Fleet.Modules.Identity.Application.Commands.ConfirmEmailCommand;

public sealed record ConfirmEmailCommand(string ConfirmationToken) : IRequest;