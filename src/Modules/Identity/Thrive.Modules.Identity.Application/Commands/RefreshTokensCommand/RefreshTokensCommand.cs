using MediatR;

namespace Thrive.Modules.Identity.Application.Commands.RefreshTokensCommand;

public sealed record RefreshTokensCommand(string RefreshToken) : IRequest;