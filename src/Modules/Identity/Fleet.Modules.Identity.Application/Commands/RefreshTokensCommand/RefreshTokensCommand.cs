using MediatR;

namespace Fleet.Modules.Identity.Application.Commands.RefreshTokensCommand;

public sealed record RefreshTokensCommand(string RefreshToken) : IRequest;